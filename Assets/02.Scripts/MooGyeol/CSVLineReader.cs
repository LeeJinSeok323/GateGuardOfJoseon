using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class CSVLineReader
{
    // CSV 데이터를 분할하는 정규식
    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    // 줄 바꿈 문자를 기준으로 CSV 파일을 분할하는 정규식
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    // 데이터에서 제거할 문자 배열
    static char[] TRIM_CHARS = { '\"' };

    // 주어진 파일에서 특정 열의 데이터를 가져오는 메소드
    public static List<string> GetColumn(string file, string columnName)
    {
        List<string> columnData = new List<string>();

        // 파일 로드
        TextAsset data = Resources.Load(file) as TextAsset;
        // 줄 단위로 분할
        var lines = Regex.Split(data.text, LINE_SPLIT_RE);

        if (lines.Length <= 1) return columnData;

        // 헤더 데이터 가져오기
        var header = Regex.Split(lines[0], SPLIT_RE);
        // 요청한 열의 인덱스 찾기
        int columnIndex = Array.IndexOf(header, columnName);

        // 요청한 열이 존재하지 않을 경우 빈 리스트 반환
        if (columnIndex == -1) return columnData;

        // 각 행을 반복하여 해당 열의 데이터 추출
        for (int i = 1; i < lines.Length; i++)
        {
            var values = Regex.Split(lines[i], SPLIT_RE);
            // 열 인덱스가 유효한 경우 데이터 추가
            if (values.Length > columnIndex)
            {
                string value = values[columnIndex].Trim(TRIM_CHARS);
                columnData.Add(value);
            }
        }

        return columnData;
    }

    // 주어진 파일에서 특정 행의 데이터를 가져오는 메소드
    public static List<string> GetRow(string file, int rowIndex)
    {
        List<string> rowData = new List<string>();

        // 파일 로드
        TextAsset data = Resources.Load(file) as TextAsset;
        // 줄 단위로 분할
        var lines = Regex.Split(data.text, LINE_SPLIT_RE);

        // 파일이 비어있거나 요청한 행이 파일의 범위를 벗어날 경우 빈 리스트 반환
        if (lines.Length <= 1 || rowIndex >= lines.Length) return rowData;

        // 요청한 행에서 데이터 추출
        var values = Regex.Split(lines[rowIndex], SPLIT_RE);
        foreach (var value in values)
        {
            string trimmedValue = value.Trim(TRIM_CHARS);
            rowData.Add(trimmedValue);
        }

        return rowData;
    }
}
