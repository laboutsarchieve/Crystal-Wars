using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapEditor.Data
{
    class ActionHistoryData
    {
        public Stack<int> numActionsToUndo;
        public Stack<MapEditAction> actionHistory;
        public Stack<int> numActionsToRedo;
        public Stack<MapEditAction> undoHistory;

        public ActionHistoryData()
        {
            actionHistory = new Stack<MapEditAction>();
            numActionsToUndo = new Stack<int>();

            undoHistory = new Stack<MapEditAction>();
            numActionsToRedo = new Stack<int>();
        }
    }
}
