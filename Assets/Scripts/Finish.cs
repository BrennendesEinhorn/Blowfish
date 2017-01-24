using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Linq;
using System.Xml;
using System.IO;
using System;

public class SemiNumericComparer : IComparer<string>
{
	public int Compare(string s1, string s2)
	{
		if (IsNumeric(s1) && IsNumeric(s2))
		{
			if (Convert.ToInt32(s1) > Convert.ToInt32(s2)) return 1;
			if (Convert.ToInt32(s1) < Convert.ToInt32(s2)) return -1;
			if (Convert.ToInt32(s1) == Convert.ToInt32(s2)) return 0;
		}

		if (IsNumeric(s1) && !IsNumeric(s2))
			return -1;

		if (!IsNumeric(s1) && IsNumeric(s2))
			return 1;

		return string.Compare(s1, s2, true);
	}

	public static bool IsNumeric(object value)
	{
		try
		{
			Convert.ToInt32(value.ToString());
			return true;
		}
		catch (FormatException)
		{
			return false;
		}
	}
}

#region ScoreBoard Model
/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class ScoreBoard
{
	public ScoreBoard()
	{
		_scoreEntries = new List<ScoreEntry>();
	}

	private List<ScoreEntry> _scoreEntries;

	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute("ScoreEntry", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
	public List<ScoreEntry> ScoreEntries
	{
		get
		{
			return this._scoreEntries;
		}
		set
		{
			this._scoreEntries = value;
		}
	}
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class ScoreEntry
{

	private string player1Field;

	private string scoreField;

	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
	public string Player1
	{
		get
		{
			return this.player1Field;
		}
		set
		{
			this.player1Field = value;
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "integer")]
	public string Score
	{
		get
		{
			return this.scoreField;
		}
		set
		{
			this.scoreField = value;
		}
	}
}
#endregion


public class Finish : MonoBehaviour 
{
	private Timer timer;
	public Text FinishText;
	private string score;
	private ScoreBoard scoreBoard = null;
	public string ScoreFileName;

	private ScoreBoard ReadScores()
	{
		XmlSerializer serializer = new XmlSerializer(typeof(ScoreBoard));
		if (File.Exists(ScoreFileName))
		{
			FileStream scoreFileStream = new FileStream(ScoreFileName, FileMode.OpenOrCreate);
			ScoreBoard scoreboard;
			try
			{
				scoreboard = (ScoreBoard) serializer.Deserialize(scoreFileStream);
			}
			catch (XmlException e)
			{
				scoreFileStream.Close();
				Debug.Log("Error while parsing scoreboard, discarding old and creating new one");
				scoreboard = new ScoreBoard();
				WriteScores(scoreboard);
				return scoreboard;
			}


			scoreFileStream.Close();
			return scoreboard;
		}
		else
		{
			return null;
		}
	}

	private void WriteScores(ScoreBoard newScores)
	{
		XmlSerializer serializer = new XmlSerializer(typeof(ScoreBoard));
		FileStream scoreFileStream = new FileStream(ScoreFileName, FileMode.OpenOrCreate);
		List<ScoreEntry> entries = newScores.ScoreEntries;
		newScores.ScoreEntries = entries.OrderBy(x => x.Score, new SemiNumericComparer()).ToList();
		newScores.ScoreEntries.Reverse();
		serializer.Serialize(scoreFileStream, newScores);
		scoreFileStream.Close();
	}

	void Start () 
	{
		FinishText.gameObject.SetActive (false);
		timer = FindObjectOfType<Timer> ();
	}

	private void OnTriggerEnter(Collider other) {

		timer.Finnish ();	
		FinishText.gameObject.SetActive (true);
		//score = timer.getEndTime ();

	}
}
