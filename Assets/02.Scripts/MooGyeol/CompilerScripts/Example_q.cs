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
        // �����Ϸ� �ʱ�ȭ ����
        bool initCompiler = true;

        // ������ ���� - C#�ڵ� �������ʿ�
        domain = ScriptDomain.CreateDomain("Mydomain", initCompiler);

        // ����� ���۷��� �߰� (��: Unity ���� �����, .NET ǥ�� ���̺귯��, ����� ���� ����� ��)
        foreach (AssemblyReferenceAsset reference in assemblyReferences)
            domain.RoslynCompilerService.ReferenceAssemblies.Add(reference);

    }

    //�ҽ��ڵ� ������ �� �ε�
    //�� �ż���� ����� ȣ���ϴ� Roslyn �����Ϸ� ȣ��
    public void Compiler()
    {
        source = Script_q.scripts;
        type = domain.CompileAndLoadMainSource(source);
        Player = GameObject.FindWithTag("Player");
        proxy = type.CreateInstance(Player);
    }

}