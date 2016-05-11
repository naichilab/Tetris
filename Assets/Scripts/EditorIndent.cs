using UnityEngine;
using UnityEditor;
using System;

public class EditorIndent:IDisposable
{
	int Indent{ get; set; }

	public EditorIndent (int indentLevel = 1)
	{
		this.Indent = indentLevel;
		EditorGUI.indentLevel += this.Indent;
	}

	public	void Dispose ()
	{
		EditorGUI.indentLevel -= this.Indent;
	}
}
