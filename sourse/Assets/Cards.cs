using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
[ExecuteInEditMode]
public class Cards : MonoBehaviour
{
    [SerializeField]
    private GameObject[] cardsM;//загруженные сплайны
    [SerializeField]
    private GameObject cardsSide;//загруженные сплайны
    [SerializeField]
    private GameObject[] spawnpoint;
    //[SerializeField]
   // private Button1 start;
    private List<GameObject> cards;//List-массив в который заносим уже созданные кубики
    private List<GameObject> cloneCards;//List-клоны кубиков
    [SerializeField]
    private int hCard = 5, wCard = 2,indexF,indexL;
    private int indexCard=0;
    private int[,] massCardIndex;
  
    //---------------------------------------------------
    private void OnMouseDown()
    {
     //   print("OnMouseDown");
        //SwithColors ();
       // OnSpheraClickHandler(SwithColors);
    }
    //---------------------------------------------------

  /*  private int Choise(int index)//3х позиционный свич 0-состояние бездействия 1-первое состояние 2-второе состояние
    {

            if (index == 0)
            {
                indexF = ChoiseC.choise;
               print("choise1 " + index + ": " + ChoiseC.choise + " " + indexF);
                index = 1;
            }else{
                if (index == 1)
                {
                    indexL = ChoiseC.choise;
                    print("choise2 " + index + ": " + ChoiseC.choise + " " + indexL);
                    index = 2;
                }
            }
            if (index == 2) {
                index = 0; 
                Debug.Log(indexF + " " + indexL); 
            }

        return indexCard;
    }*/
    //---------------------------------------------------

      private int Choise(int index)//3х позиционный свич 0-состояние бездействия 1-первое состояние 2-второе состояние
      {

          if (indexCard == 0)
              {
                  indexF = ChoiseC.choise;
                //  print("choise1 " + indexCard + ": " + ChoiseC.choise + " " + indexF);
                  indexCard = 1;
              }else{
                  if (indexCard == 1)
                  {
                      indexL = ChoiseC.choise;
                    //  print("choise2 " + indexCard + ": " + ChoiseC.choise + " " + indexL);
                      indexCard = 2;
                  }
              }
              if (indexCard == 2)
              {
                  indexCard = 0; 
                //  Debug.Log(indexF + " " + indexL); 
              }

          return indexCard;
      }
      //-----------------------------------------------------------------  
      private void Vivod()
      {
          for (int i = 0; i < hCard; i++)//находим куда будем записывать карту
          {
              for (int k = 0; k < wCard; k++)
              {     
    //                  print(string.Format("massCardIndex[{0}, {1}] = {2}", i, k,massCardIndex[i, k]));                                              
              }
          }         
      }
    //---------------------------------------------------
      private void Start()
      {       
          cards = new List<GameObject>();
          cloneCards = new List<GameObject>();
          massCardIndex = new int[hCard, wCard];      //создаем главный массив в котором будут все положения кубика занесены        
          FullMassRand();
          Vivod();
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
                //  print("2");
                  if (massCardIndex[i, k]== 0) {
                      

                      int CardDouble = RandCard(0, massCardIndex.Length);//опр рандомно карту котарая будет записываться
                         massCardIndex[i, k] = CardDouble;
                         print("CardDouble" + CardDouble);
                         index = 1;
                      if (index == 1) { 
                       //   PoiskMesta( CardDouble);
                          index = 0;
                      }
                  }
                  
            //      print(string.Format("massCardIndex[{0}, {1}] = {2}", i, k,massCardIndex[i, k]));
              }
          }
       /*   RandCard(10,20);
                int CardDouble=RandCard(0, massCardIndex.Length);
              if(massCardIndex[i, k]==null)
              {                
                  massCardIndex[i, k] = CardDouble;
                  PoiskMesta(CardDouble);
              }*/
          
      }
      //-----------------------------------------------------------------  
      private void PoiskMesta(int CardDouble)//поиск места для карты дубля
      {
          int i = RandCard(0, hCard-1);
          int k = RandCard(0, wCard-1);
          int Cdouble = CardDouble;
          //int indexIter = 0;
          print(string.Format("i {0}, k {1}, hCard {2} , wCard {3}, CardDouble {4}, Cdouble {5}", i, k, hCard, wCard, CardDouble, Cdouble));
        
        if (massCardIndex[i, k] == 0)
          {
              massCardIndex[i, k] = CardDouble;
             // indexIter = 0;
          }
          else {
             // if (indexIter < 20)
            //  {
              PoiskMesta(Cdouble);
                 // indexIter++;
           //   }             
          }
           
      }
  
      //-----------------------------------------------------------------  
      private int RandCard(int min ,int max)
      {
          print("rand " + UnityEngine.Random.Range(min,max));
          return UnityEngine.Random.Range(min, max); 
      }
      //-----------------------------------------------------------------  

      private void RandStand(int NCard, Vector3 position)  //CreateKubik(число на кубике, позиция);  
         { 
         var CardIgrok = Instantiate(cardsM[NCard - 1]) as GameObject;
            CardIgrok.transform.position = position;
            cloneCards.Add(CardIgrok);
      }
      //---------------------------------------------------
    private void Update()
    {
        /*   if (Input.GetKeyDown(KeyCode.Mouse0) && currentIndex < balls.Length)
           {
               //	var temp = balls [currentIndex].gameObject.GetComponent<Rigidbody> ();
               //	temp.isKinematic = false;
               //	temp.AddForce (transform.up * balls [currentIndex].speed, balls [currentIndex].forceMode);
               //	currentIndex++;
               balls[currentIndex].Fly();
               currentIndex++;
           }*/
      
      //  Debug.Log(indexF + " " + indexL);
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (ChoiseC.choise != null)
            {
                
                  //  indexCard = 1;
                    if (indexCard == 0)
                    {
                        indexF = ChoiseC.choise;
                        print("choise1 " + indexCard + ": " + ChoiseC.choise + " " + indexF);
                        indexCard=1;
                    }
                    else {
                        if (indexCard == 1)
                        {
                            indexL = ChoiseC.choise;
                            print("choise2 " + indexCard + ": " + ChoiseC.choise + " " + indexL);
                            indexCard = 2;
                        }
                    }
                    if (indexCard == 2) {
                    
                        if (indexF != null && indexL != null)
                        {
                            Debug.Log(indexF + " " + indexL);
                            GameCard(cardsM[indexF], cardsM[indexL]);
                        }
                        indexCard = 0;
                        //  Debug.Log(indexF + " " + indexL);
                    }               
            }        
        }
    }
    //---------------------------------------------------
    private void GameCard(GameObject firstC, GameObject lastCard)
    {
        if (firstC == lastCard)
        {
            print("!!!!");
            Destroy();
        }
        else
        {
            print("-----");
            // firstC=Quaternion.RotateTowards();
            //  lastCard=Quaternion.RotateTowards();
        }
    }
    //---------------------------------------------------
    private void Destroy()
    {


}
//---------------------------------------------------

    private void RotateCard(GameObject obj)
    {
        //  Quaternion.RotateTowards();
    }

}
