﻿using UnityEngine;
using System.Collections;

public class TextScript : MonoBehaviour {

	public string[] HStr = 
	{
		/* 0=== */ "",
		/* 1 */ "",
		/* 2 */ "",
		/* 3 */ "",
		/* 4 */ "",

		/* 5 */ "",
		/* 6 */ "",
		/* 7 */ "",
		/* 8 */ "",
		/* 9 */ "",

		/* 10=== */ 
		"이 파이프는 9시방향과 6시방향을 이어주는 파이프이며 물은 양방향으로 흐를 수 있다.",
		"이 파이프는 9시방향과 12시방향을 이어주는 파이프이며 물은 양방향으로 흐를 수 있다.",
		"이 파이프는 12시방향과 3시방향을 이어주는 파이프이며 물은 양방향으로 흐를 수 있다.",
		"이 파이프는 3시방향과 6시방향을 이어주는 파이프이며 물은 양방향으로 흐를 수 있다.",
		"이 파이프는 9시방향과 3시방향을 이어주는 파이프이며 물은 양방향으로 흐를 수 있다.",
		
		"이 파이프는 12시방향과 6시방향을 이어주는 파이프이며 물은 양방향으로 흐를 수 있다.",
		"이 파이프는 9시방향과 6시방향, 12시방향과 6시방향을 이어주는 파이프이며 물은 양방향으로 흐를 수 있다.",
		"",
		"",
		"",

		/* 20=== */ 
		"이 파이프는 9시방향과 6시방향을 이어주며 물은 화살표 방향으로만 흐를수 있다.",
		"이 파이프는 9시방향과 12시방향을 이어주며 물은 화살표 방향으로만 흐를수 있다.",
		"이 파이프는 12시방향과 3시방향을 이어주며 물은 화살표 방향으로만 흐를수 있다.",
		"이 파이프는 3시방향과 6시방향을 이어주며 물은 화살표 방향으로만 흐를수 있다.",
		"이 파이프는 9시방향과 3시방향을 이어주며 물은 화살표 방향으로만 흐를수 있다.",

		"이 파이프는 12시방향과 6시방향을 이어주며 물은 화살표 방향으로만 흐를수 있다.",
		"",
		"",
		"",
		"",

		/* 30=== */ 
        "이 파이프는 9시방향과 6시방향을 이어주며 물은 화살표 방향으로만 흐를수 있다.",
		"이 파이프는 9시방향과 12시방향을 이어주며 물은 화살표 방향으로만 흐를수 있다.",
		"이 파이프는 12시방향과 3시방향을 이어주며 물은 화살표 방향으로만 흐를수 있다.",
		"이 파이프는 3시방향과 6시방향을 이어주며 물은 화살표 방향으로만 흐를수 있다.",
		"이 파이프는 9시방향과 3시방향을 이어주며 물은 화살표 방향으로만 흐를수 있다.",

		"이 파이프는 12시방향과 6시방향을 이어주며 물은 화살표 방향으로만 흐를수 있다.",
		"",
		"",
		"",
		"",

		/* 40=== */ 
		"이 파이프는 9시방향과 6시방향을 이어주는 [FF0000]고정[-]파이프이며 물은 양방향으로 흐를 수 있다.",
		"이 파이프는 9시방향과 12시방향을 이어주는 [FF0000]고정[-]파이프이며 물은 양방향으로 흐를 수 있다.",
		"이 파이프는 12시방향과 3시방향을 이어주는 [FF0000]고정[-]파이프이며 물은 양방향으로 흐를 수 있다.",
		"이 파이프는 3시방향과 6시방향을 이어주는 [FF0000]고정[-]파이프이며 물은 양방향으로 흐를 수 있다.",
		"이 파이프는 9시방향과 3시방향을 이어주는 [FF0000]고정[-]파이프이며 물은 양방향으로 흐를 수 있다.",
		
		"이 파이프는 12시방향과 6시방향을 이어주는 [FF0000]고정[-]파이프이며 물은 양방향으로 흐를 수 있다.",
		"이 파이프는 9시방향과 6시방향, 12시방향과 6시방향을 이어주는 [FF0000]고정[-]파이프이며 물은 양방향으로 흐를 수 있다.",
		"",
		"",
		"",

		/* 50=== */ 
		"이 파이프는 9시방향과 6시방향을 이어주는 [FF0000]고정[-]파이프이며 물은 화살표 방향으로만 흐를수 있다.",
		"이 파이프는 9시방향과 12시방향을 이어주는 [FF0000]고정[-]파이프이며 물은 화살표 방향으로만 흐를수 있다.",
		"이 파이프는 12시방향과 3시방향을 이어주는 [FF0000]고정[-]파이프이며 물은 화살표 방향으로만 흐를수 있다.",
		"이 파이프는 3시방향과 6시방향을 이어주는 [FF0000]고정[-]파이프이며 물은 화살표 방향으로만 흐를수 있다.",
		"이 파이프는 9시방향과 3시방향을 이어주는 [FF0000]고정[-]파이프이며 물은 화살표 방향으로만 흐를수 있다.",

		"이 파이프는 12시방향과 6시방향을 이어주는 [FF0000]고정[-]파이프이며 물은 화살표 방향으로만 흐를수 있다.",
		"",
		"",
		"",
		"",

		/* 60=== */ 
        "이 파이프는 9시방향과 6시방향을 이어주는 [FF0000]고정[-]파이프이며 물은 화살표 방향으로만 흐를수 있다.",
		"이 파이프는 9시방향과 12시방향을 이어주는 [FF0000]고정[-]파이프이며 물은 화살표 방향으로만 흐를수 있다.",
		"이 파이프는 12시방향과 3시방향을 이어주는 [FF0000]고정[-]파이프이며 물은 화살표 방향으로만 흐를수 있다.",
		"이 파이프는 3시방향과 6시방향을 이어주는 [FF0000]고정[-]파이프이며 물은 화살표 방향으로만 흐를수 있다.",
		"이 파이프는 9시방향과 3시방향을 이어주는 [FF0000]고정[-]파이프이며 물은 화살표 방향으로만 흐를수 있다.",

		"이 파이프는 12시방향과 6시방향을 이어주는 [FF0000]고정[-]파이프이며 물은 화살표 방향으로만 흐를수 있다.",
		"",
		"",
		"",
		"",

		/* 70=== */ 
        "12시방향으로만 들어올 수 있는 물을 화살표 방향으로 나누어 주는 파이프이다.",
        "3시방향으로만 들어올 수 있는 물을 화살표 방향으로 나누어 주는 파이프이다.",
        "6시방향으로만 들어올 수 있는 물을 화살표 방향으로 나누어 주는 파이프이다.",
        "9시방향으로만 들어올 수 있는 물을 화살표 방향으로 나누어 주는 파이프이다.",
        "9시, 3시로 들어온 물을 12시 화살표 방향으로 합쳐 주는 파이프이다.",

        "12시, 6시로 들어온 물을 3시 화살표 방향으로 합쳐 주는 파이프이다.",
        "9시, 3시로 들어온 물을 6시 화살표 방향으로 합쳐 주는 파이프이다.",
        "12시, 6시로 들어온 물을 9시 화살표 방향으로 합쳐 주는 파이프이다.",
        "",
        "",
		

		/* 80=== */ 
        "12시방향으로만 들어올 수 있는 물을 화살표 방향으로 나누어 주는 파이프이다.",
        "3시방향으로만 들어올 수 있는 물을 화살표 방향으로 나누어 주는 파이프이다.",
        "6시방향으로만 들어올 수 있는 물을 화살표 방향으로 나누어 주는 파이프이다.",
        "9시방향으로만 들어올 수 있는 물을 화살표 방향으로 나누어 주는 파이프이다.",
        "9시, 6시, 3시로 들어온 물을 12시 화살표 방향으로 합쳐 주는 파이프이다.",

        "12시, 9시, 6시로 들어온 물을 3시 화살표 방향으로 합쳐 주는 파이프이다.",
        "9시, 12시, 3시로 들어온 물을 6시 화살표 방향으로 합쳐 주는 파이프이다.",
        "12시, 3시, 6시로 들어온 물을 9시 화살표 방향으로 합쳐 주는 파이프이다.",
        "",
        "",

		/* 90=== */ 
        "12시방향으로만 들어올 수 있는 물을 화살표 방향으로 나누어 주는 [FF0000]고정[-]파이프이다.",
        "3시방향으로만 들어올 수 있는 물을 화살표 방향으로 나누어 주는 [FF0000]고정[-]파이프이다.",
        "6시방향으로만 들어올 수 있는 물을 화살표 방향으로 나누어 주는 [FF0000]고정[-]파이프이다.",
        "9시방향으로만 들어올 수 있는 물을 화살표 방향으로 나누어 주는 [FF0000]고정[-]파이프이다.",
        "9시, 3시로 들어온 물을 12시 화살표 방향으로 합쳐 주는 [FF0000]고정[-]파이프이다.",

        "12시, 6시로 들어온 물을 3시 화살표 방향으로 합쳐 주는 [FF0000]고정[-]파이프이다.",
        "9시, 3시로 들어온 물을 6시 화살표 방향으로 합쳐 주는 [FF0000]고정[-]파이프이다.",
        "12시, 6시로 들어온 물을 9시 화살표 방향으로 합쳐 주는 [FF0000]고정[-]파이프이다.",
        "",
        "",

		/* 100=== */ 
        "12시방향으로만 들어올 수 있는 물을 화살표 방향으로 나누어 주는 [FF0000]고정[-]파이프이다.",
        "3시방향으로만 들어올 수 있는 물을 화살표 방향으로 나누어 주는 [FF0000]고정[-]파이프이다.",
        "6시방향으로만 들어올 수 있는 물을 화살표 방향으로 나누어 주는 [FF0000]고정[-]파이프이다.",
        "9시방향으로만 들어올 수 있는 물을 화살표 방향으로 나누어 주는 [FF0000]고정[-]파이프이다.",
        "9시, 6시, 3시로 들어온 물을 12시 화살표 방향으로 합쳐 주는 [FF0000]고정[-]파이프이다.",

        "12시, 9시, 6시로 들어온 물을 3시 화살표 방향으로 합쳐 주는 [FF0000]고정[-]파이프이다.",
        "9시, 12시, 3시로 들어온 물을 6시 화살표 방향으로 합쳐 주는 [FF0000]고정[-]파이프이다.",
        "12시, 3시, 6시로 들어온 물을 9시 화살표 방향으로 합쳐 주는 [FF0000]고정[-]파이프이다.",
        "",
        "",

		/* 110=== */ 
        "",
		"",
		"",
		"",
		"",
		
		"",
		"",
		"",
		"",
		"",

		/* 120=== */ 
        "",
		"",
		"",
		"",
		"",
		
		"",
		"",
		"",
		"",
		"",

		/* 130=== */ 
        "인 파이프로 들어간 물은 아웃 파이프로 나온다. 인/아웃 파이프는 같은 색상끼리 작동한다. ",
		"",
		"",
		"",
		"",
		
		"",
		"",
		"",
		"",
		"",

		/* 140=== */ 
        "",
		"",
		"",
		"",
		"",
		
		"",
		"",
		"",
		"",
		"",

		/* 150=== */ 
        "",
		"",
		"",
		"",
		"",
		
		"",
		"",
		"",
		"",
		"",

		/* 160=== */ "",
		/* 1 */ "",
		/* 2 */ "",
		/* 3 */ "",
		/* 4 */ "",
		
		/* 5 */ "",
		/* 6 */ "",
		/* 7 */ "",
		/* 8 */ "",
		/* 9 */ "",

		/* 170=== */ 
        "월 인/아웃 파이프는 항상 마주보고 있는 파이프끼리 작동한다. 물이 들어가면 마주보고 있는 월 인/아웃 파이프로 물이 나온다.",
        "",
        "",
        "",
        "",

        "",
        "",
        "",
        "",
        "",
		
	};

	public string[] PName = 
	{
		/* 0=== */ "",
		/* 1 */ "",
		/* 2 */ "",
		/* 3 */ "",
		/* 4 */ "",
		
		/* 5 */ "",
		/* 6 */ "",
		/* 7 */ "",
		/* 8 */ "",
		/* 9 */ "",
		
		/* 10=== */ 
		"LeftBottom",
		"LeftTop",
		"RightTop",
		"RightBottom",
		"Horizontal",
		
		"Vertical",
		"Cross",
		"",
		"",
		"",
		
		/* 20=== */ 
		"LeftBottom - 9to6",
		"LeftTop - 12to9",
		"RightTop - 3to12",
		"RightBottom - 6to3",
		"Horizontal - 3to9",
		
		"Vertical - 6to12",
		"",
		"",
		"",
		"",
		
		/* 30=== */ 
		"LeftBottom - 6to9",
		"LeftTop - 9to12",
		"RightTop - 12to3",
		"RightBottom - 3to6",
		"Horizontal - 9to3",
		
		"Vertical - 12to6",
		"",
		"",
		"",
		"",
		
		/* 40=== */ 
		"LeftBottom (F)",
		"LeftTop (F)",
		"RightTop (F)",
		"RightBottom (F)",
		"Horizontal (F)",
		
		"Vertical (F)",
		"Cross (F)",
		"",
		"",
		"",
		
		/* 50=== */ 
		"LeftBottom - 9to6 (F)",
		"LeftTop - 12to9 (F)",
		"RightTop - 3to12 (F)",
		"RightBottom - 6to3 (F)",
		"Horizontal - 3to9 (F)",
		
		"Vertical - 6to12 (F)",
		"",
		"",
		"",
		"",
		
		/* 60=== */ 
		"LeftBottom - 6to9 (F)",
		"LeftTop - 9to12 (F)",
		"RightTop - 12to3 (F)",
		"RightBottom - 3to6 (F)",
		"Horizontal - 9to3 (F)",
		
		"Vertical - 12to6 (F)",
		"",
		"",
		"",
		"",
		
		/* 70=== */ 
		"In 12 - Out 3,9",
		"In 3 - Out 12,6",
		"In 6 - Out 3,9",
		"In 9 - Out 12,6",
		"In 3,9 - Out 12",
		
		"In 12,6 - Out 3",
		"In 3,9 - Out 6",
		"In 12,6 - Out 9",
		"",
		"",
		
		
		/* 80=== */ 
		"In 12 - Out 3,6,9",
		"In 3 - Out 6,9,12",
		"In 6 - Out 3,9,12",
		"In 9 - Out 3,6,12",
		"In 3,6,9 - Out 12",
		
		"In 6,9,12 - Out 3",
		"In 3,9,12 - Out 6",
		"In 3,6,12 - Out 9",
		"",
		"",
		
		/* 90=== */ 
		"In 12 - Out 3,9 (F)",
		"In 3 - Out 12,6 (F)",
		"In 6 - Out 3,9 (F)",
		"In 9 - Out 12,6 (F)",
		"In 3,9 - Out 12 (F)",
		
		"In 12,6 - Out 3 (F)",
		"In 3,9 - Out 6 (F)",
		"In 12,6 - Out 9 (F)",
		"",
		"",
		
		/* 100=== */ 
		"In 12 - Out 3,6,9 (F)",
		"In 3 - Out 6,9,12 (F)",
		"In 6 - Out 3,9,12 (F)",
		"In 9 - Out 3,6,12 (F)",
		"In 3,6,9 - Out 12 (F)",
		
		"In 6,9,12 - Out 3 (F)",
		"In 3,9,12 - Out 6 (F)",
		"In 3,6,12 - Out 9 (F)",
		"",
		"",
		
		/* 110=== */ 
		"",
		"",
		"",
		"",
		"",
		
		"",
		"",
		"",
		"",
		"",
		
		/* 120=== */ 
		"",
		"",
		"",
		"",
		"",
		
		"",
		"",
		"",
		"",
		"",
		
		/* 130=== */ 
		"In 6",
		"In 12",
		"In 9",
		"In 3",
		"Out 6",
		
		"Out 12",
		"Out 9",
		"Out 3",
		"",
		"",
		
		/* 140=== */ 
		"In 6",
		"In 12",
		"In 9",
		"In 3",
		"Out 6",
		
		"Out 12",
		"Out 9",
		"Out 3",
		"",
		"",
		
		/* 150=== */ 
		"In 6",
		"In 12",
		"In 9",
		"In 3",
		"Out 6",
		
		"Out 12",
		"Out 9",
		"Out 3",
		"",
		"",
		
		/* 160=== */ "",
		/* 1 */ "",
		/* 2 */ "",
		/* 3 */ "",
		/* 4 */ "",
		
		/* 5 */ "",
		/* 6 */ "",
		/* 7 */ "",
		/* 8 */ "",
		/* 9 */ "",
		
		/* 170=== */ 
		"Wall Left In/Out",
		"Wall Top In/Out",
		"Wall Bottom In/Out",
		"Wall Right In/Out",
		"",
		
		"",
		"",
		"",
		"",
		"",
		
	};

	private static TextScript _instance;
	
	/// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// 
	// Static singleton property
	public static TextScript Instance
	{
		// Here we use the ?? operator, to return 'instance' if 'instance' does not equal null
		// otherwise we assign instance to a new component and return that
		get 
		{ 
			if(_instance == null)
			{
				GameObject obj = new GameObject ();
				obj.hideFlags = HideFlags.HideAndDontSave;
				_instance = obj.AddComponent<TextScript> ();
			}
			
			return _instance;
		}
	}


	public string getStr(int _index)
	{
		return HStr[_index];
	}
}
