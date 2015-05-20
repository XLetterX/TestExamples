using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]

public class GameController : MonoBehaviour
{
	[SerializeField] private Button[]buttons;	
    [SerializeField] private  int hCard = 3, wCard = 2;	
	[SerializeField] private Vector2 startPosition;
	[SerializeField] private GameObject spawnpoint;
	[SerializeField] private int lengthCard;
	[SerializeField] private List<GameObject> cards;//List-массив в который заносим уже созданные кубики
    private float widthElement = 250f, heigthElement = 250f;	
	private List<GameObject> cloneCards;//List-клоны кубиков
	private List<GameObject> cloneSpawn;//List-клоны кубиков
	private int[,] massCardIndex;  
    private int indexF, indexL, indexIter,indexCard = 0;

    //-----------------------------------------------------------------    
    private void OnGUI()
    {  
       if (GUI.Button(new Rect(Screen.width - 100, Screen.height - 50, 100, 50), "Пуск"))
        {
            Debug.Log("!!!!!!!!");
        }
    }
    //----------------------------------------------------------------------------------------------------------------
    private void Start() {
        
        cloneCards = new List<GameObject>();
        cloneSpawn = new List<GameObject>();
        massCardIndex = new int[hCard, wCard];      //создаем главный массив для рандомно распределенных карт       
        MassSpawn();//создаем массив по распределению места
        FullMassRand();//рандомно раскидываем по массиву 
        MassCard(); 
    }
	//----------------------------------------------------------------- 
		private void Awake ()
	{
				foreach (var item in buttons) {
						item.OnButtonClick += HandleOnButtonClick;//выделяем  HandleOnButtonClick нажимаем h и enter
				}
		}
	//----------------------------------------------------------------- 
		void HandleOnButtonClick (Card card)
		{
				if (card == null) {
						return;
				}
                //------------         
          /*      cloneCards = new List<GameObject>();
                cloneSpawn = new List<GameObject>();
                massCardIndex = new int[hCard, wCard];      //создаем главный массив для рандомно распределенных карт       
                MassSpawn();//создаем массив по распределению места
                FullMassRand();//рандомно раскидываем по массиву 
                MassCard();  */
               // Debug.Log("cards.Count"+cards.Count);

               // Vivod();
                //-----------------
				card.Flip ();
		        Choise (card);
     
		//Debug.Log(card);
		}
	//----------------------------------------------------------------- 
		private void OnDestroy ()
		{
				foreach (var item in buttons) {
						item.OnButtonClick -= HandleOnButtonClick;
				}
		}	
	//----------------------------------------------------------------- 
	
    #region CREATE_CARD
        //----------------------------------------------------------------- 
    private void MassCard()
    {
        int n = 0;            
        for (int i = 0; i < hCard; i++)
        {
            for (int j = 0; j < wCard; j++)
            {                   
                CreateCard(SearchAnalogCard(massCardIndex[i, j]), cloneSpawn[n].transform.position);  
                n++;               
            }
        }
    }
    //----------------------------------------------------------------- 
    private GameObject SearchAnalogCard(int card) //передается имя карты и ищется соответствующая ему карта и листа кард
    {
        foreach (var item in cards)
        {
            if (card ==ParseToInt(item.name))
            {
                return item;
            }
        }
        return null;
    }
    //----------------------------------------------------------------- 
    private void CreateCard(GameObject cards,Vector3 itemPosition)
        {//CreateSpawn(число на кубике, позиция);  

            var CardPP = Instantiate(cards) as GameObject;
            CardPP.transform.position = itemPosition;
            cloneCards.Add(CardPP);
		var temp = CardPP.GetComponent<Button> ();
		temp.OnButtonClick += HandleOnButtonClick;
	    }
    #endregion     
    //----------------------------------------------------------------- 
    #region SPAWN_Create
    private void MassSpawn(){
        for (int i = 0; i < hCard; i++)
		{
            for (int j = 0; j < wCard; j++)				
			{				
				CreateSpawn(new Vector3(startPosition.x+widthElement*i,startPosition.y-heigthElement*j,spawnpoint.transform.position.z));
			}
		}
		}
	//----------------------------------------------------------------- 
    private void CreateSpawn(Vector3 itemPosition)
    {//CreateSpawn(число на кубике, позиция);  		
		var SpawnPP = Instantiate(spawnpoint) as GameObject;
		SpawnPP.transform.position = itemPosition;
		cloneSpawn.Add(SpawnPP);		
	}
    #endregion
    //-----------------------------------------------------------------	
    #region RAND_CARD
        private int RandCard(int min, int max)
        {
            //	print("rand " + UnityEngine.Random.Range(min,max));
            return UnityEngine.Random.Range(min, max);
        }
        //-----------------------------------------------------------------  
        private void FullMassRand()
	    {
		    int index =0;
		    //  m = 0;
		    for (int i = 0; i < hCard; i++)//находим куда будем записывать карту
		    {
			    for (int k = 0; k < wCard; k++)
			    {				   
				    if (massCardIndex[i, k]== 0) {

                        int CardDouble = RandCard(1, cards.Count);//опр рандомно карту котарая будет записываться
                       // Debug.Log(massCardIndex.Length);
					    massCardIndex[i, k] = CardDouble;				  
					    index = 1;
					    if (index == 1) { 
	    					   PoiskMesta( CardDouble);
						    index = 0;
					    }
				    }				
				    //      print(string.Format("massCardIndex[{0}, {1}] = {2}", i, k,massCardIndex[i, k]));
			    }
		    }
		  
	    }
	    //-----------------------------------------------------------------  
	    private void PoiskMesta(int CardDouble)//поиск места для карты дубля
	    {
		    int i = RandCard(0, hCard);
		    int k = RandCard(0, wCard);
		
		    if (massCardIndex[i, k] == 0)
		    {
			    massCardIndex[i, k] = CardDouble;
			    indexIter = 0;
		    }
		    else {
			    if (massCardIndex[i, k] != 0)
			      {
				    PoiskMesta(CardDouble);
				     indexIter++;
				    if (indexIter>massCardIndex.Length) return;
			    }           
		    }		
	    }
    #endregion
    //-----------------------------------------------------------------  
    #region Utils
       
	    private void Vivod()
	    {
		    for (int i = 0; i < hCard; i++)//находим куда будем записывать карту
		    {
			    for (int k = 0; k < wCard; k++)
			    {     
				   print(string.Format("[{0}, {1}] = {2}", i, k,massCardIndex[i, k]));                                              
			    }
		    }         
	    }

	    //-------------------------------------------------	//Превращает str в int
	    private int ParseToInt(string someText)
	    {
		    int temp;
		    if (int.TryParse(someText, out temp))
		    {
			    return temp;
		    }
		    return temp;		    
	    }
    #endregion
    //---------------------------------------------------------------
    #region GLogik_Search_rigth_result
        
	    private void Choise (Card card)
	    {
		
		  
		    if (card != null)
		    {			    
			    if (indexCard == 0)
			    {				   
				    indexF =ParseToInt( card.name);
				 //   print("choise1 " + indexCard + ": " + card.name + " " + indexF);
				    indexCard=1;
			    }
			    else {
				    if (indexCard == 1)
				    {					   
					    indexL = ParseToInt(card.name);
					 //   print("choise2 " + indexCard + ": " + card.name + " " + indexL);
					    indexCard = 2;
				    }
			    }
			    if (indexCard == 2) {
				
				    if (indexF != null && indexL != null)
				    {
					  //  Debug.Log(indexF + " " + indexL);					    
					    GameCard(indexF,indexL);
				    }
				    indexCard = 0;				 
			    }               
		    }  
	    }
	    //---------------------------------------------------	
	    private void GameCard(int firstC, int lastCard)
	    {
		    if (firstC == lastCard)
		    {
			    print("!!!!");
			    //Debug.Log(buttons[firstC] + " " + buttons[lastCard]);
			    Debug.Log(firstC+ " " + lastCard);
			    //buttons[firstC].enabled=false;
			    //buttons[lastCard].enabled=false;
		    //	Destroy();
		    }
		    else
		    {
			    print("-----");
			    // firstC=Quaternion.RotateTowards();
			    //  lastCard=Quaternion.RotateTowards();
		    }
	    }
    //---------------------------------------------------
    #endregion
}