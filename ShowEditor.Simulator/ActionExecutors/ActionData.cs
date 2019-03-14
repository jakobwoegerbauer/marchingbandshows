using ShowEditor.Data;
using ShowEditor.Simulator.ExecutionGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowEditor.Simulator.ActionExecutors
{
    public class ActionData
    {
        public int Time { get; private set; }
        public int LocalTime { get; private set; }
        public int CurrentPlayer { get; }

        private readonly PositionHistory[] players;
        private readonly int[] positionMapping;
        private readonly Element element;

        public Dictionary<string, object> ActionParameters { get; private set; }

        public ActionData(int time, int localTime, PositionHistory[] players, int[] positionMapping, int currentPlayer, Element element, Dictionary<string, object> actionParameters)
        {
            Time = time;
            LocalTime = localTime;
            this.players = players;
            CurrentPlayer = currentPlayer;
            this.positionMapping = positionMapping;
            this.element = element;
            ActionParameters = actionParameters ?? new Dictionary<string, object>();
        }

        public Formation GetFormation()
        {
            return element.StartFormation;
        }

        public Position GetPosition(int localTimeStamp)
        {
            return GetPosition(CurrentPlayer, localTimeStamp);
        }

        public Position GetPosition(int player, int localTimeStamp)
        {
            return GetPositionGlobalTime(player, localTimeStamp + Time - LocalTime);
        }

        public Position GetCurrentPosition()
        {
            return GetCurrentPosition(CurrentPlayer);
        }

        public Position GetCurrentPosition(int player)
        {
            return GetPositionGlobalTime(player, Time);
        }

        private Position GetPositionGlobalTime(int player, int globalTime)
        {
            return players[positionMapping[player]].GetPosition(globalTime);
        }
    }
}
