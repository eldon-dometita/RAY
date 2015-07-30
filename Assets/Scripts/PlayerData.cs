using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class PlayerData : MonoBehaviour{

	//static PlayerData _instance;
	bool bDEBUG_01 = false;
	bool bDEBUG_02 = false;
	bool bDEBUG_03 = false;
	

	public const int EXTERNAL_ITEM_MAX = 15;
	public const int PIPE_ITEM_MAX = 23;
	public int invenOpenLevel = 1;				//controll....this

	public const int GOLD_SHOP_ITEM_MAX = 8;
	public const int CONSUME_ITEM_MAX = 10;


	bool bRead = false;
	public int gold;
	public int gem;
	public int[] pipes;
	public int[] consumeItem;
	//public int[] temppipes;

	//public int nowMapIndex;
	//public int nowStageIndex;
	public int invenMax;				//posMaxCount
	public const int consumeMax = 999;
	public float invenCoolTime;
	public float flowTime;
	public int flowDefaultTime;			//save file time

	public int life;
	public int bUnlimitTime;
	public long saveUnlimitTime;
	public long saveLifeTime;

	public int nowMapNumber;
	public static int nowMapNumber1;		//temp
	public static int nowPlanet;

	//0 -> 120sec
	//1 -> 150sec
	//2 -> 180sec
	//3 -> 210sec
	//4 -> 240sec
	//5 -> 270sec
	public int waterTimePriceIndex = 0;	

	public int iSound;
	public int iVibration;

	public byte[] isOpen;
	public int[] record;	//every stage record
	public byte[] star;		//how many get star
	public byte[] LimitNorm;
	public short[] LimitTime;
	public int[] map_maxScore;

	public static int STAGE_SIZE = 500;

	long currentTime;
	public int lifeTime;

	public int lastStage;

	// This field can be accesed through our singleton instance,
	// but it can't be set in the inspector, because we use lazy instantiation
//	public int number;

	public static bool bOneInit = false;

	// Static singleton instance
	private static PlayerData _instance;
	
	// Static singleton property
	public static PlayerData Instance
	{
		// Here we use the ?? operator, to return 'instance' if 'instance' does not equal null
		// otherwise we assign instance to a new component and return that
		get 
		{ 
			if(_instance == null)
			{

//				_instance = new GameObject("PlayerData").AddComponent<PlayerData>(); 
//				_instance.Init();
					GameObject obj = new GameObject ();
					obj.hideFlags = HideFlags.HideAndDontSave;
					_instance = obj.AddComponent<PlayerData> ();
					_instance.Init();
					_instance.loadNorm_N_Score();
					//DontDestroyOnLoad(obj);
			}

			return _instance;
		}
	}

//	void Awake()
//	{
//		if (_instance == null) {
//						_instance = FindObjectOfType<PlayerData> ();
//						if (_instance == null) {
//								GameObject obj = new GameObject ();
//								obj.hideFlags = HideFlags.HideAndDontSave;
//								_instance = obj.AddComponent<PlayerData> ();
//								_instance.Init ();
//				DontDestroyOnLoad(obj);
//						} else {
//								Destroy (_instance.gameObject);
//						}
//				}
//	}




//	void Awake()
//	{
//		Debug.Log ("Awake--------------------------------------------");
//		if (_instance == null) {
//			Debug.Log ("if(instance == null)================================");
//			_instance = this;
//			_instance.Init();
//			DontDestroyOnLoad (this);
//		} else {
//			if(this != _instance)
//			{
//				Destroy(this.gameObject);
//			}
//		}
//	}
	// Instance method, this method can be accesed through the singleton instance
//	public void DoSomeAwesomeStuff()
//	{
//		Debug.Log("I'm doing awesome stuff");
//	}


	
	// Use this for initialization
	void Init() 
	{
//		if (!bOneInit) {
						
		if (!bRead) 
		{

				iSound = 1;
				iVibration = 1;


				record = new int[STAGE_SIZE];
				star = new byte[STAGE_SIZE];
				isOpen = new byte[STAGE_SIZE];
				LimitNorm = new byte[STAGE_SIZE];
				LimitTime = new short[STAGE_SIZE];
				map_maxScore = new int[STAGE_SIZE];
				for (int i=0; i<STAGE_SIZE; i++) {
						record [i] = 0;
						star [i] = 0;
						isOpen [i] = 0;
						map_maxScore[i] = 0;
				}
				isOpen [0] = 1;

				invenMax = 3;	//posMaxCount
				invenOpenLevel = 6; // default pipe open
				//invenOpenLevel = 7; // default pipe open
				//invenOpenLevel = 15;	// 1to2 2to1 pipe open
				//invenOpenLevel = 23;	// 1to3 3to1 pipe open
				pipes = new int[PIPE_ITEM_MAX];
				for (int i=0; i<PIPE_ITEM_MAX; i++) {
						pipes [i] = invenMax;
						//pipes [i] = Random.Range(0, invenMax-5);
				}


				consumeItem = new int[CONSUME_ITEM_MAX];
				for (int i=0; i<CONSUME_ITEM_MAX; i++) {
						consumeItem [i] = 3;
				}

				gold = 10000;
				gem = 50;
				life = 5;

				invenCoolTime = 1.0f;

				nowMapNumber = 0;
				lastStage = 0;
				//nowPlanet = 0;

				string path = pathForDocumentsFile ("save.dat");

				if (bDEBUG_01) {
						bUnlimitTime = 1;
						saveUnlimitTime = System.DateTime.UtcNow.ToFileTimeUtc ();
						saveData ();
				}

				//			Debug.Log("saved");
				saveData ();
				if (File.Exists (path)) {
						//				Debug.Log("loaded");
						loadData ();

						calHeart ();
				} else {
						//Debug.Log("saveData");
						saveData ();
				}


				flowDefaultTime = 120;
				if (bDEBUG_02) {
						flowDefaultTime = 5;
				}

				flowTime = flowDefaultTime;

				bRead = true;
		}
//			bOneInit = true;
//				}
	}

	private void loadNorm_N_Score() 
	{	
		//int shortValue = 0;
		//int byteValue = 0;
		
//		int width = 0;
//		int height = 0;
//		int emptySpace = 0;
//		
//		int mapdata = 0;
//		int perfectplan = 0;
//		int clearType = 0;
		
		try 
		{
			
			
			TextAsset textAssets = Resources.Load("MAP_DATA/Norm") as TextAsset;
			
			Stream stream = new MemoryStream(textAssets.bytes);
			
			using(BinaryReader br = new BinaryReader(stream))
			{
				
				for(int i=0; i<STAGE_SIZE; i++)
				{
					LimitNorm[i] = br.ReadByte();
					map_maxScore[i] = br.ReadInt32();
					LimitTime[i] = br.ReadInt16();

				}
			}
		} 
		catch(IOException ex)
		{
			Debug.Log(ex.Message);
		}
		
	}

	public void calHeart()
	{

		currentTime = DateTime.UtcNow.ToFileTimeUtc ();
		long tempTime = currentTime - saveLifeTime;
		lifeTime = (int)((tempTime) / 10000000);
		
		if (lifeTime > GameThread.seconde_Life_Minute * 5) 
		{
			if(life < 5)
			{
				life = 5;
				saveLifeTime = currentTime;
				saveData ();
			}
		}
		else if (lifeTime > GameThread.seconde_Life_Minute * 4) 
		{
			if(life < 5)
			{
				life = 4;
				saveLifeTime = currentTime - tempTime;
				saveData ();
			}
		}
		else if (lifeTime > GameThread.seconde_Life_Minute * 3) 
		{
			if(life < 5)
			{
				life = 3;
				saveLifeTime = currentTime - tempTime;
				saveData ();
			}
		}
		else if (lifeTime > GameThread.seconde_Life_Minute * 2) 
		{
			if(life < 5)
			{
				life = 2;
				saveLifeTime = currentTime - tempTime;
				saveData ();
			}
		}
		else if (lifeTime > GameThread.seconde_Life_Minute * 1) 
		{
			if(life < 5)
			{
				life = 1;
				saveLifeTime = currentTime - tempTime;
				saveData ();
			}
		}
	}

	public void saveData() 
	{	
		//int tempValue = 0;
		string path = pathForDocumentsFile( "save.dat" );
		try 
		{
			FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
			BinaryWriter bw = new BinaryWriter(fs);

			for(int i=0; i<PIPE_ITEM_MAX; i++)
			{
				bw.Write(pipes[i]);
			}

			for(int i=0; i<STAGE_SIZE; i++)
			{
				bw.Write(record[i]);
			}

			for(int i=0; i<STAGE_SIZE; i++)
			{
				bw.Write(star[i]);
			}

			for(int i=0; i<STAGE_SIZE; i++)
			{
				bw.Write(isOpen[i]);
			}

//			for(int i=0; i<STAGE_SIZE; i++)
//			{
//				bw.Write(LimitNorm[i]);
//			}

			for(int i=0; i<CONSUME_ITEM_MAX; i++)
			{
				bw.Write(consumeItem[i]);
			}

			bw.Write(invenMax);
			bw.Write(invenCoolTime);
			bw.Write(flowDefaultTime);
			bw.Write(gold);
			bw.Write(gem);

			bw.Write(nowMapNumber);

			bw.Write(waterTimePriceIndex);
			bw.Write(invenOpenLevel);

			bw.Write(iSound);
			bw.Write(iVibration);

			bw.Write(bUnlimitTime);
			bw.Write(saveUnlimitTime);
			bw.Write(saveLifeTime);

			bw.Close ();
			fs.Close ();
		}
		catch(IOException ex)
		{
			Debug.Log(ex.Message);
		}
	}

	public void loadData() 
	{	
		//int tempValue = 0;
		string path = pathForDocumentsFile( "save.dat" );
		try 
		{
			FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read);
			BinaryReader br = new BinaryReader(fs);
			
			for(int i=0; i<PIPE_ITEM_MAX; i++)
			{
				pipes[i] = br.ReadInt32();
//				pipes[i] = 10;
			}
			
			for(int i=0; i<STAGE_SIZE; i++)
			{
				record[i] = br.ReadInt32();
			}
			
			for(int i=0; i<STAGE_SIZE; i++)
			{
				star[i] = br.ReadByte();
			}

			for(int i=0; i<STAGE_SIZE; i++)
			{
				isOpen[i] = br.ReadByte();
			}

//			for(int i=0; i<STAGE_SIZE; i++)
//			{
//				LimitNorm[i] = br.ReadByte();
//			}
			
			for(int i=0; i<CONSUME_ITEM_MAX; i++)
			{
				consumeItem[i] = br.ReadInt32();
				//consumeItem[i] = i+10;
			}
			
			invenMax = br.ReadInt32();
			invenCoolTime = br.ReadInt32();
			flowDefaultTime = br.ReadInt32();
			gold = br.ReadInt32();
			gem = br.ReadInt32();


			
			nowMapNumber = br.ReadInt32();

			waterTimePriceIndex = br.ReadInt32();
			invenOpenLevel = br.ReadInt32();



			iSound = br.ReadInt32();
			iVibration = br.ReadInt32();

			bUnlimitTime = br.ReadInt32();
			saveUnlimitTime = br.ReadInt64();
			saveLifeTime = br.ReadInt64();

			br.Close ();
			fs.Close ();
		}
		catch(IOException ex)
		{
			Debug.Log(ex.Message);
		}
		
	}
	
	public string pathForDocumentsFile( string filename ) 
	{ 
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			string path = Application.persistentDataPath.Substring( 0, Application.persistentDataPath.Length - 5 );
			path = path.Substring( 0, path.LastIndexOf( '/' ) );
			return Path.Combine( Path.Combine( path, "Documents" ), filename );
		}
		else if(Application.platform == RuntimePlatform.Android)
		{
			string path = Application.persistentDataPath; 
			path = path.Substring(0, path.LastIndexOf( '/' ) ); 
			return Path.Combine (path, filename);
		} 
		else 
		{
			string path = Application.dataPath; 
			path = path.Substring(0, path.LastIndexOf( '/' ) );
			return Path.Combine (path, filename);
		}
	}




//	// Update is called once per frame
//	void Update () {
//	
//	}
}


