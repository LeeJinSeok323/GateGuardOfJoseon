using UnityEngine;
using RoslynCSharp;
using static Unity.VisualScripting.Member;
using RoslynCSharp.Compiler;

public class Example_q : MonoBehaviour
{
    private ScriptDomain domain = null;
    public AssemblyReferenceAsset[] assemblyReferences;
    //public AssemblyReferenceAsset referenceAsset;
    ScriptProxy proxy = null;
    ScriptType type = null;
    private GameObject Player;

    string source = "";

    private void Start()
    {
        // 컴파일러 초기화 여부
        bool initCompiler = true;

        // 도매인 생성 - C#코드 컴파일필요
        domain = ScriptDomain.CreateDomain("Mydomain", initCompiler);

        // 어셈블리 레퍼런스 추가 (예: Unity 엔진 어셈블리, .NET 표준 라이브러리, 사용자 정의 어셈블리 등)
        foreach (AssemblyReferenceAsset reference in assemblyReferences)
            domain.RoslynCompilerService.ReferenceAssemblies.Add(reference);

    }

    //소스코드 컴파일 및 로드
    //위 매서드는 어셈블리 호출하는 Roslyn 컴파일러 호출
    public void Compiler()
    {
        source = Script_q.scripts;
        type = domain.CompileAndLoadMainSource(source);
        Player = GameObject.FindWithTag("Player");
        proxy = type.CreateInstance(Player);
    }

}