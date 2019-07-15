using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Days
{
    public class Day09 : Day
    {
        public Day09()
        {
            Part1Text = "Winning score";
            Part2Text = "";

            Load("inputs/day09.txt");
        }

        static long PlayGame(int maxPlayers, int maxMarbleNumber)
        {
            Dictionary<int, long> playerScores = new Dictionary<int, long>();
            LinkedList<int> marbleRing = new LinkedList<int>();
            LinkedListNode<int> current = marbleRing.AddFirst(0);

            for (int i = 0; i < maxMarbleNumber; i++)
            {
                int player = i % maxPlayers + 1;
                int marble = i + 1;
                LinkedListNode<int> insertPoint = null;

                if ((marble % 23) == 0)
                {
                    if (playerScores.ContainsKey(player))
                    {
                        playerScores[player] += marble;
                    }
                    else
                    {
                        playerScores.Add(player, marble);
                    }
                    for (int j = 0; j < 7; j++)
                    {
                        current = current.Previous ?? current.List.Last;
                    }
                    insertPoint = current;
                    playerScores[player] += insertPoint.Value;
                    current = insertPoint.Next ?? current.List.First;
                    marbleRing.Remove(insertPoint.Value);
                }
                else
                {
                    insertPoint = current.Next ?? current.List.First;
                    current = marbleRing.AddAfter(insertPoint, marble);
                }
            }
            long maxScore = long.MinValue;
            foreach (KeyValuePair<int, long> kvp in playerScores)
            {
                maxScore = Math.Max(maxScore, kvp.Value);
            }
            return maxScore;
        }

        override public void Solve()
        {
            string[] splitted = Input[0].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int maxPlayers = int.Parse(splitted[0]);
            int maxMarbleNumber = int.Parse(splitted[6]);
            
            Part1Solution = CircularLinkedList.PlayGame2(maxPlayers, maxMarbleNumber).ToString();
            Part2Solution = CircularLinkedList.PlayGame2(maxPlayers, maxMarbleNumber * 100).ToString();
        }
    }

    public class CircularLinkedList
    {
        public CircularLinkListNode ActiveNode { get; set; }

        public int NodeCount { get; set; }

        public CircularLinkedList()
        {
            ActiveNode = new CircularLinkListNode() { Marble = 0L };
            ActiveNode.Clockwise = ActiveNode;
            ActiveNode.Counterclockwise = ActiveNode;

            NodeCount = 1;
        }

        public void GameMarble(long marble)
        {
            var c1Node = ActiveNode.Clockwise;
            var c2Node = c1Node.Clockwise;

            var nNode = new CircularLinkListNode() { Marble = marble, Clockwise = c2Node, Counterclockwise = c1Node };
            c1Node.Clockwise = nNode;
            c2Node.Counterclockwise = nNode;
            ActiveNode = nNode;
        }

        public long Game23k(long marble)
        {
            long score = marble;

            var remNode = ActiveNode.Counterclockwise.Counterclockwise.Counterclockwise.Counterclockwise.Counterclockwise.Counterclockwise.Counterclockwise;

            score += remNode.Marble;

            var rcc = remNode.Counterclockwise;
            var rc = remNode.Clockwise;

            rcc.Clockwise = rc;
            rc.Counterclockwise = rcc;
            ActiveNode = rc;

            return score;
        }

        public static long PlayGame2(int players, long lastMarble)
        {
            long[] playerScores = new long[players];

            int activePlayer = 0;

            CircularLinkedList list = new CircularLinkedList();

            for (long marble = 1L; marble <= lastMarble; marble++)
            {
                if (marble % 23L == 0L)
                {
                    playerScores[activePlayer] += list.Game23k(marble);
                }
                else
                {
                    list.GameMarble(marble);
                }

                activePlayer = ((activePlayer + 1) % players);
            }

            return playerScores.Max();
        }
    }

    public class CircularLinkListNode
    {
        public CircularLinkListNode Counterclockwise { get; set; }

        public CircularLinkListNode Clockwise { get; set; }

        public long Marble { get; set; }
    }
}
