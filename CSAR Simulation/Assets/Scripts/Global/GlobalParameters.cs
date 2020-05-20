using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalParameters
{
    static class TaskDefinition
    {

    }

    static class EnemyDefinition
    {

    }

    static class EquipmentSelection
    {
        public static YDYH ydyh = YDYH.A_10;
        public static SAR sar = SAR.MH_53;
    }

    static class A_10
    {

    }

    static class MH_53
    {

    }

    static class AC_130
    {

    }

    static class MH_60
    {

    }

    enum YDYH
    {
        A_10,
        AC_130
    }

    enum SAR
    {
        MH_53,
        MH_60
    }
}
