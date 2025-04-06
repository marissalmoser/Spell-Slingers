using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        Queue<int> turnOrder = new Queue<int>();

        turnOrder.Enqueue(1);
        turnOrder.Enqueue(2);
        turnOrder.Enqueue(3);

        Debug.Log(turnOrder);

        Debug.Log(turnOrder.Dequeue());
        turnOrder.Enqueue(1);

        //Debug.Log(turnOrder.Dequeue().Call Jacob's thing);
        Debug.Log(turnOrder.Dequeue());
    }




    /*public static void main()
    {
        Queue<string> Players = new Queue<string>();
        Players.Enqueue("Player1");
        Players.Enqueue("Player2");
        Players.Enqueue("Player3");

        // A queue can be enumerated without disturbing its contents.
        foreach( string number in Players)
        {
            Console.WriteLine(number);
        }

        Console.WriteLine("\nDequeing '{0}'", Players.Dequeue());
        Console.WriteLine("Peek at next item to dequeue: {0}", Players.Peek());
        Console.WriteLine("Dequeuing '{0}'", Players.Dequeue());

        //Creating a copy of the queue, using the To Array method and the
        // constructor that accepts an IEnumerable<T>.
        Queue<string> queueCopy = new Queue<string>(Players.ToArray());

        Console.WriteLine("/nContents of the first copy:");
        foreach( string number in queueCopy)
        {
            Console.WriteLine(number);
        }

        string[] array2 = new string[Players.Count * 2];
        Players.CopyTo(array2, Players.Count);

        Console.WriteLine("\nContents of the second copy, with duplicates and nulls")
    }*/
}
