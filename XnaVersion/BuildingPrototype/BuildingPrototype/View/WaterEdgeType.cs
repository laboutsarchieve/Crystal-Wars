using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapEditor.View
{
    enum WaterEdgeType
    {
        // Land on Four sides
        WaterIsland, 

        // Land on Three Sides
        LowerChannelEnd,
        UpperChannelEnd,
        RightChannelEnd,
        LeftChannelEnd,
        ChannelVertical,
        ChannelHorizontal,  

        // Land on Two sides
        UpperLeft,        
        LowerLeft,        
        UpperRight,        
        LowerRight,
        MiddleLeft,
        MiddleRight,
        UpperMiddle,
        LowerMiddle,
              
        // Land on no sides, but one diagonal land
        UpperRightTip,
        UpperLeftTip,
        LowerRightTip,
        LowerLeftTip,

        // No land on sides or diagonal
        Center
    }
}
