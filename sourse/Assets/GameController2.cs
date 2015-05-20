using UnityEngine;
using System;

using System.Text;
using System.Linq;
using System.Threading;

using System.Collections;
using System.Collections.Generic;

public class GameController2 : MonoBehaviour {
    [SerializeField]
    private Button[] buttons;
    [SerializeField]
    private int hCard = 3, wCard = 2;
    [SerializeField]
    private Vector2 startPosition;
    [SerializeField]
    private GameObject spawnpoint;
   // [SerializeField]
   // private int lengthCard;
    [SerializeField]
    private List<GameObject> cards;//List-массив в который заносим уже созданные кубики
    [SerializeField]
    private int widthElement = 3, heigthElement = 4;  
    private List<GameObject> cloneCards;//List-клоны кубиков
    private List<GameObject> cloneSpawn;//List-клоны кубиков
    private int[,] massCardIndex;
    private int indexIter, indexCard = 0;
	private int indexF=0, indexL=0,iFCard,iLCard,Vinner;

    //-----------------------------------------------------------------    
    private void OnGUI()
    {
        if (GUI.Button(new Rect(Screen.width - 100, Screen.height - 50, 100, 50), "Пуск"))
        {
            Start1();
        }
		if (Vinner == 1) 
		{
			GUI.TextField(new Rect(Screen.width /2, Screen.height/2, 300, 200), "Вы выиграли!!!");
				}
    }   
    //----------------------------------------------------------------- 
	// Use this for initialization
	private void Start1 () {
    
        DestroyGameObject(cloneCards);
        DestroyGameObject(cloneSpawn);
      //  massCardIndex.GetLength(0);
		Vinner = 0;

        cloneCards = new List<GameObject>();
        cloneSpawn = new List<GameObject>();
        massCardIndex = new int[hCard, wCard];      //создаем главный массив для рандомно распределенных карт  



           MassSpawn();//создаем массив по распределению места
           FullMassRand();//рандомно раскидываем по массиву 
            MassCard();
		foreach (var item in cloneCards) {
			var temp =item.GetComponent<Card>();
			temp.Flip();
		}
        //Vivod();
    
	}
    //----------------------------------------------------------------- 
    private void Awake()
    {
        foreach (var item in buttons)
        {
            item.OnButtonClick += HandleOnButtonClick;//выделяем  HandleOnButtonClick нажимаем h и enter
        }
    }
    //----------------------------------------------------------------- 
    void HandleOnButtonClick(Card card)
    {
        if (card == null)
        {
            return;
        }       
        card.Flip();   
        
        Choise(card);
    }
    //----------------------------------------------------------------- 
    private void OnDestroy()
    {
        foreach (var item in buttons)
        {
            item.OnButtonClick -= HandleOnButtonClick;
        }
    }
    //----------------------------------------------------------------- 
    private void DestroyGameObject(List<GameObject> temp)
    {
        if (temp != null)
        {
            for (int i = 0; i < temp.Count; i++)
            {
                Destroy(temp[i]);
            }
        }
    }
    //----------------------------------------------------------------- 
    private void DestroyGameObject(GameObject temp)
    {
        if (temp != null)
        {
            Destroy(temp);
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
            if (card == ParseToInt(item.name))
            {
                return item;
            }
        }
        return null;
    }
    //----------------------------------------------------------------- 
    private void CreateCard(GameObject cards, Vector3 itemPosition)
    {//CreateSpawn(число на кубике, позиция);  

        var CardPP = Instantiate(cards) as GameObject;
        CardPP.transform.position = itemPosition;
        cloneCards.Add(CardPP);
        var temp = CardPP.GetComponent<Button>();
        temp.OnButtonClick += HandleOnButtonClick;
    }
    #endregion
    //----------------------------------------------------------------- 
    #region SPAWN_Create
    private void MassSpawn()
    {
        for (int i = 0; i < hCard; i++)
        {
            for (int j = 0; j < wCard; j++)
            {
                CreateSpawn(new Vector3(startPosition.x + widthElement * i, startPosition.y - heigthElement * j, spawnpoint.transform.position.z));
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
        int index = 0;
        //  m = 0;
        for (int i = 0; i < hCard; i++)//находим куда будем записывать карту
        {
            for (int k = 0; k < wCard; k++)
            {
                if (massCardIndex[i, k] == 0)
                {

                    int CardDouble = RandCard(1, cards.Count);//опр рандомно карту котарая будет записываться
                    // Debug.Log(massCardIndex.Length);
                    massCardIndex[i, k] = CardDouble;
                    index = 1;
                    if (index == 1)
                    {
                        PoiskMesta(CardDouble);
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
        else
        {
            if (massCardIndex[i, k] != 0)
            {
                PoiskMesta(CardDouble);
                indexIter++;
                if (indexIter > massCardIndex.Length) return;
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
                print(string.Format("[{0}, {1}] = {2}", i, k, massCardIndex[i, k]));
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
    //----------------------------------------------------------------- 
    #region GLogik_Search_rigth_result

    private void Choise(Card card)
    {
         char[] seps = { '(',')' };

        if (card != null)
        {
            if (indexCard == 0)
            {         
                string[] strM =(card.name).Split(seps);//разбиваем по заданным символам и записываем в массив 
                indexF = ParseToInt(strM[0]); //определяем имя объекта
				iFCard=FindCard(card);//определяем место его в Листе
			//	print("choise1 " + indexCard + ": " + card.name + " " + indexF+ " iFCard " + iFCard);
                indexCard = 1;
            }
            else
            {
                if (indexCard == 1)
                {
                    string[] strM = (card.name).Split(seps);//разбиваем по заданным символам и записываем в массив 
					indexL = ParseToInt(strM[0]);     //определяем имя объекта
					iLCard=FindCard(card);//определяем место его в Листе
				//	print("choise2 " + indexCard + ": " + card.name + " " + indexL+ " iLCard " + iLCard);
                    indexCard = 2;
                }
            }
            if (indexCard == 2)
            {
				if (iFCard!= iLCard)
              //  if (indexF != 0 && indexL !=0)
                {
                   //   Debug.Log(indexF + " " + indexL);					    
					GameCard(indexF, indexL,iFCard,iLCard);                   
                }
                indexCard = 0;
            }
        }
    }
	//---------------------------------------------------	
	private int FindCard(Card card){
		int i = 0;
		foreach (var item in cloneCards) 
		{					
			if (item==card.gameObject)			
			{
				return i;
			}
			i++;
		}
		return 0;
	}
    //---------------------------------------------------	
	private void GameCard(int firstC, int lastCard,int iFCard,int iLCard)
 
    {
        if (firstC == lastCard)
        {
			Debug.Log("!!!!! " + firstC + " " + lastCard+" FCard "+iFCard+" iLCard "+iLCard);


			DestroyGameObject(cloneCards[iFCard]);		
		//	cloneCards.RemoveAt(iFCard);
			DestroyGameObject(cloneCards[iLCard]);
        }
        else
        {

			var temp =cloneCards[iFCard].GetComponent<Card>();
			temp.Flip();
			int y=1;
			if (y==1){
				var temp1 =cloneCards[iLCard].GetComponent<Card>();
				temp1.Flip(); 
				y=0;
			}
			 
          
        }
		int m=1;
		foreach (var item in cloneCards) 
			{		
		//	if(cloneCards.)
				if (item == null) {
				  Debug.Log("m " +m +" cloneCards.Count " + cloneCards.Count);	
						
						if (m+2 == cloneCards.Count) {
								Debug.Log ("Вы выиграли!!!");
					Vinner=1;
						}
				m++;
				}
			}
    }
    //---------------------------------------------------
    #endregion
    //----------------------------------------------------------------- 	
}
