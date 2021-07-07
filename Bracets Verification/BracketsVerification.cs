﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Bracets_Verification 
{
     public class BracketsVerification
     {
        private static string allowableValues = "{}()[]";

        public List<int> checkInputValidation(string stringToCheck)
        {
            Console.WriteLine(stringToCheck);
            checkInputNotEmpty(stringToCheck);
            checkInputContainsNonBracketsValue(stringToCheck);
            return checkContainsUnmatchedBrackets(stringToCheck);

        } 
        //метод для проверки строки на пустоту, если она пустая - метод генерирует исключение.
        private void checkInputNotEmpty(string stringToCheck)
        {
            if (String.IsNullOrEmpty(stringToCheck))
            {
                Console.WriteLine("String is empty");
                throw new ArgumentException($"String is empty");
            }  
        }
        //метод для проверки строки на недопустимые символы, если она есть - метод генерирует исключение и уведомляет о недопустимых символах.
        private void checkInputContainsNonBracketsValue(string stringToCheck)
        {
            string unexpectedCharacters = ""; //строка которая будет содержать недопустимые символы, чтоб вывести их в консоль
            for (int i = 0; i < stringToCheck.Length; i++)
            {
                if (!allowableValues.Contains(stringToCheck[i]))
                {
                    unexpectedCharacters += "'" + stringToCheck[i] + "'" + " ";
                }
            }
            if (unexpectedCharacters != "")
            {
                Console.WriteLine($"A characters {unexpectedCharacters} doesn’t belong to any known brackets type ");
                throw new ArgumentException($"A characters doesn’t belong to any known brackets type ");
            }
                
        }
        //метод для проверки строки на то, что все открытые скобки имеют закрывающие скобки, если такие скобки присутствуют,
        //метод возвращает список индексов всех несбалансированных скобок начиная с нуля, если все скобки имеют закрывающую скобку, метод возвращает список с индексом -1.
        //В тестовом задание есть неточность, так как в части Functional Description сказано " Output: integer value returns  -1 - when balanced and number of bracket when not balanced "
        //но в части "Acceptance Criteria: " otherwise print - NOT BALANCED and return a number of not balanced brackets in the line. " а именно return number of brackets 
        // и мною было принято решение лучше возвращать list of integer.
        private List<int> checkContainsUnmatchedBrackets(string stringToCheck)
        {
            int count1 = 0; // счетчик для { }
            int count2 = 0; // счетчик для ( )
            int count3 = 0; // счетчик для [ ]
            Stack<int> unmatchedBracketsIndexesFirstType = new Stack<int>();   // стэк нужен для того, чтоб проследить все ли открывающие скобки имеют закрывающую скобку.
            Stack<int> unmatchedBracketsIndexesSecondType = new Stack<int>();  // если стэк оказывается в конце пустой, проверка на то, что строка не содержит непарных скобок выполнена
            Stack<int> unmatchedBracketsIndexesThirdType = new Stack<int>();


            List<int> listForReturningByApplication = new List<int>(); // по заданию, нужно вернуть number of brackets
                                                                       // " otherwise print - NOT BALANCED and return a number of not balanced brackets in the line. "
                                                                       // таким образом, мое приложение будет возвращать индексы всех целочисленных непарных скобок 

            for (int i = 0; i < stringToCheck.Length; i++)
            {
                    switch (stringToCheck[i])
                {
                    case '{':
                        count1++;
                        unmatchedBracketsIndexesFirstType.Push(i); // добавляю в очередь индекс открывающей скобки, если он не покинет очередь, значит скобка незакрывающаяся
                        break;

                    case '}':
                        count1--;
                        try
                        {
                            unmatchedBracketsIndexesFirstType.Pop(); 
                        }
                        catch 
                        {
                            listForReturningByApplication.Add(i);
                            // если стэк пустой, при попытке удаления оттуда закрывающей скобки
                            // она автоматически попадает в список несбалансированых скобок.
                            // Тоесть если строка начнется с закрывающейся скобки } то, она будет несбалансированой. 
                            // или ()} 
                            //       ↑ сейчас  стэк пустой , поэтому она попадет в список несбалансированных скобок.
                        }
                        break;

                    case '(':
                        count2++;
                        unmatchedBracketsIndexesSecondType.Push(i);
                        break;
                    case ')':
                        count2--;
                        try
                        {
                            unmatchedBracketsIndexesSecondType.Pop();
                        }
                        catch 
                        {
                            listForReturningByApplication.Add(i);
                            
                        }
                        break;

                    case '[':
                        count3++;
                        unmatchedBracketsIndexesThirdType.Push(i);
                        break;
                    case ']':
                        count3--;
                        try
                        {
                            unmatchedBracketsIndexesThirdType.Pop();
                        }
                        catch 
                        {
                            listForReturningByApplication.Add(i);
                            
                        }
                        break; 
                }
            }
            
            // три цикла нужны для проверки, не остались ли в конце прошлого цикла неудаленные из стека индексы
            
            foreach (var e in unmatchedBracketsIndexesFirstType)
            {
                listForReturningByApplication.Add(e);
            }
            foreach (var e in unmatchedBracketsIndexesSecondType)
            {
                listForReturningByApplication.Add(e);
            }
            foreach (var e in unmatchedBracketsIndexesThirdType)
            {
                listForReturningByApplication.Add(e);
            }
            if (listForReturningByApplication.Count != 0)
            {
                Console.Write($"NOT BALANCED (");
                foreach (var e in listForReturningByApplication)
                {
                    Console.Write(e + " ");
                }
                Console.Write(")");
                return listForReturningByApplication;
            }
            else
            {
                Console.WriteLine("BALANCED");
                listForReturningByApplication.Add(-1);
                return listForReturningByApplication;
            }
              
        }
       
     }
}

