using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class CSVLineReader
{
    // CSV �����͸� �����ϴ� ���Խ�
    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    // �� �ٲ� ���ڸ� �������� CSV ������ �����ϴ� ���Խ�
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    // �����Ϳ��� ������ ���� �迭
    static char[] TRIM_CHARS = { '\"' };

    // �־��� ���Ͽ��� Ư�� ���� �����͸� �������� �޼ҵ�
    public static List<string> GetColumn(string file, string columnName)
    {
        List<string> columnData = new List<string>();

        // ���� �ε�
        TextAsset data = Resources.Load(file) as TextAsset;
        // �� ������ ����
        var lines = Regex.Split(data.text, LINE_SPLIT_RE);

        if (lines.Length <= 1) return columnData;

        // ��� ������ ��������
        var header = Regex.Split(lines[0], SPLIT_RE);
        // ��û�� ���� �ε��� ã��
        int columnIndex = Array.IndexOf(header, columnName);

        // ��û�� ���� �������� ���� ��� �� ����Ʈ ��ȯ
        if (columnIndex == -1) return columnData;

        // �� ���� �ݺ��Ͽ� �ش� ���� ������ ����
        for (int i = 1; i < lines.Length; i++)
        {
            var values = Regex.Split(lines[i], SPLIT_RE);
            // �� �ε����� ��ȿ�� ��� ������ �߰�
            if (values.Length > columnIndex)
            {
                string value = values[columnIndex].Trim(TRIM_CHARS);
                columnData.Add(value);
            }
        }

        return columnData;
    }

    // �־��� ���Ͽ��� Ư�� ���� �����͸� �������� �޼ҵ�
    public static List<string> GetRow(string file, int rowIndex)
    {
        List<string> rowData = new List<string>();

        // ���� �ε�
        TextAsset data = Resources.Load(file) as TextAsset;
        // �� ������ ����
        var lines = Regex.Split(data.text, LINE_SPLIT_RE);

        // ������ ����ְų� ��û�� ���� ������ ������ ��� ��� �� ����Ʈ ��ȯ
        if (lines.Length <= 1 || rowIndex >= lines.Length) return rowData;

        // ��û�� �࿡�� ������ ����
        var values = Regex.Split(lines[rowIndex], SPLIT_RE);
        foreach (var value in values)
        {
            string trimmedValue = value.Trim(TRIM_CHARS);
            rowData.Add(trimmedValue);
        }

        return rowData;
    }
}
