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
        private readonly PositionHistory[] players;
        private readonly int currentPlayer;
        private readonly int[] positionMapping;

        public Dictionary<string, object> ActionParameters { get; private set; }

        public ActionData(int time, int localTime, PositionHistory[] players, int[] positionMapping, int currentPlayer, Dictionary<string, object> actionParameters)
        {
            Time = time;
            LocalTime = localTime;
            this.players = players;
            this.currentPlayer = currentPlayer;
            this.positionMapping = positionMapping;
            ActionParameters = actionParameters ?? new Dictionary<string, object>();
        }

        public Position GetPosition(int localTimeStamp)
        {
            return GetPosition(currentPlayer, localTimeStamp);
        }

        public Position GetPosition(int player, int localTimeStamp)
        {
            return GetPositionGlobalTime(player, localTimeStamp + Time - LocalTime);
        }

        public Position GetCurrentPosition()
        {
            return GetCurrentPosition(currentPlayer);
        }

        public Position GetCurrentPosition(int player)
        {
            return GetPositionGlobalTime(player, Time+1);
        }

        private Position GetPositionGlobalTime(int player, int globalTime)
        {
            return players[positionMapping[player]].GetPosition(globalTime);
        }
    }
}
