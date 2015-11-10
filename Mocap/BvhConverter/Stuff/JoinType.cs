
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BvhConverter_new.Stuff
{
    // Summary:
    //     This contains all of the possible joint types.
    public enum JointType
    {
        // Summary:
        //     The center of the hip.
        HipCenter = 0,
        //
        // Summary:
        //     The bottom of the root-cross.
        CrossUp,
        //
        // Summary:
        //     The bottom of the spine.
        Spine,
        //
        // Summary:
        //     The center of the shoulders.
        ShoulderCenter,
        //
        // Summary:
        //     The players head.
        Head,
        //
        // Summary:
        //     The left shoulder.
        ShoulderLeft,
        //
        // Summary:
        //     The left elbow.
        ElbowLeft,
        //
        // Summary:
        //     The left wrist.
        WristLeft,
        //
        // Summary:
        //     The left hand.
        HandLeft,
        //
        // Summary:
        //     The right shoulder.
        ShoulderRight,
        //
        // Summary:
        //     The right elbow.
        ElbowRight,
        //
        // Summary:
        //     The right wrist.
        WristRight,
        //
        // Summary:
        //     The right hand.
        HandRight,
        //
        // Summary:
        //     The left of the root-cross.
        CrossLeft,
        //
        // Summary:
        //     The left hip.
        HipLeft,
        //
        // Summary:
        //     The left knee.
        KneeLeft,
        //
        // Summary:
        //     The left ankle.
        AnkleLeft,
        //
        // Summary:
        //     The left foot.
        FootLeft,
        //
        // Summary:
        //     The right of the root-cross.
        CrossRight,
        //
        // Summary:
        //     The right hip.
        HipRight,
        //
        // Summary:
        //     The right knee.
        KneeRight,
        //
        // Summary:
        //     The right ankle.
        AnkleRight = 18,
        //
        // Summary:
        //     The right foot.
        FootRight = 19,
    }
}
