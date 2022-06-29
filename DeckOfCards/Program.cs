
namespace DeckOfCards
{
	using System;
	using System.Linq;
	using System.Collections.Generic;
	using System.Text.RegularExpressions;

	namespace PlayCards
	{
		public class Hand
		{
			public static void Main()
			{
				DeckOfCards deck;
				deck = new DeckOfCards();
				deck.shuffle(100);
				List<string>[] player = new List<string>[5];
				for (int h = 0; h < 5; h++)
					player[h] = new List<string>();
				for (int i = 0; i < 5; i++)
					for (int j = 0; j < 5; j++)
						player[j].Add(deck.deal().toString());
				for (int k = 0; k < 5; k++)
				{
					List<string> sortedList = player[k].OrderBy(x => PadNumbers(x)).OrderBy(g => FaceValue(g)).ToList();
					Console.WriteLine("Player #" + (k + 1) + ": " + string.Join("-", sortedList));
				}
			}

			public static string PadNumbers(string input)
			{
				return Regex.Replace(input, "[0-9]+", match => match.Value.PadLeft(2, '0'));
			}

			public static int FaceValue(string input)
			{
				return input.Substring(0, 1) == "A" ? 14 : input.Substring(0, 1) == "K" ? 13 : input.Substring(0, 1) == "Q" ? 12 : input.Substring(0, 1) == "J" ? 11 : 0;
			}
		}

		public class Card
		{
			public static readonly String[] Rank = { "*", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };
			private static readonly String[] Suit = { "*", "D", "C", "H", "S" };
			private byte cardSuit;
			private byte cardRank;
			public Card(int suit, int rank)
			{
				if (rank == 1)
					cardRank = 14;
				else
					cardRank = (byte)rank;
				cardSuit = (byte)suit;
			}

			public int suit()
			{
				return (cardSuit);
			}

			public int rank()
			{
				return (cardRank);
			}

			public String toString()
			{
				return (Rank[cardRank] + Suit[cardSuit]);
			}
		}

		public class DeckOfCards
		{
			private const int NCARDS = 52;
			private Card[] deckOfCards;
			private int currentCard;
			private Random randNum;
			public DeckOfCards()
			{
				deckOfCards = new Card[NCARDS];
				int i = 0;
				for (int suit = 1; suit <= 4; suit++)
					for (int rank = 1; rank <= 13; rank++)
						deckOfCards[i++] = new Card(suit, rank);
				currentCard = 0;
			}

			public void shuffle(int n)
			{
				int i, j;
				randNum = new Random();
				for (int k = 0; k < n; k++)
				{
					i = (int)(randNum.Next(NCARDS));
					j = (int)(randNum.Next(NCARDS));
					Card tmp = deckOfCards[i];
					deckOfCards[i] = deckOfCards[j];
					deckOfCards[j] = tmp;
				}

				currentCard = 0;
			}

			public Card deal()
			{
				if (currentCard < NCARDS)
					return (deckOfCards[currentCard++]);
				else
					return (null);
			}
		}
	}
}
