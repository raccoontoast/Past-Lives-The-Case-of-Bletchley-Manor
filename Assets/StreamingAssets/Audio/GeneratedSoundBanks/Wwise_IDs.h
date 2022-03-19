/////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Audiokinetic Wwise generated include file. Do not edit.
//
/////////////////////////////////////////////////////////////////////////////////////////////////////

#ifndef __WWISE_IDS_H__
#define __WWISE_IDS_H__

#include <AK/SoundEngine/Common/AkTypes.h>

namespace AK
{
    namespace EVENTS
    {
        static const AkUniqueID EXPLORING = 1823678183U;
        static const AkUniqueID IN_GAME = 2967546505U;
        static const AkUniqueID MUSIC = 3991942870U;
        static const AkUniqueID PUZZLE_SUCCESS = 2687737065U;
        static const AkUniqueID PUZZLING = 346692864U;
        static const AkUniqueID TRIGGER_KEY_PICKUP = 1983107610U;
        static const AkUniqueID TRIGGER_LIFE_CHANGE = 442340631U;
    } // namespace EVENTS

    namespace STATES
    {
        namespace GAMEPLAY
        {
            static const AkUniqueID GROUP = 89505537U;

            namespace STATE
            {
                static const AkUniqueID EXPLORING = 1823678183U;
                static const AkUniqueID MENU = 2607556080U;
                static const AkUniqueID NONE = 748895195U;
                static const AkUniqueID PUZZLING = 346692864U;
            } // namespace STATE
        } // namespace GAMEPLAY

    } // namespace STATES

    namespace SWITCHES
    {
        namespace PUZZLING
        {
            static const AkUniqueID GROUP = 346692864U;

            namespace SWITCH
            {
                static const AkUniqueID PUZZLE_SUCCESS = 2687737065U;
                static const AkUniqueID PUZZLING = 346692864U;
            } // namespace SWITCH
        } // namespace PUZZLING

    } // namespace SWITCHES

    namespace GAME_PARAMETERS
    {
        static const AkUniqueID KEY_DISTANCE = 361584188U;
        static const AkUniqueID LIFE_ONE_DISTANCE = 2755879116U;
        static const AkUniqueID LIFE_TWO_DISTANCE = 2935852738U;
        static const AkUniqueID PLAYBACK_RATE = 1524500807U;
        static const AkUniqueID RPM = 796049864U;
        static const AkUniqueID SS_AIR_FEAR = 1351367891U;
        static const AkUniqueID SS_AIR_FREEFALL = 3002758120U;
        static const AkUniqueID SS_AIR_FURY = 1029930033U;
        static const AkUniqueID SS_AIR_MONTH = 2648548617U;
        static const AkUniqueID SS_AIR_PRESENCE = 3847924954U;
        static const AkUniqueID SS_AIR_RPM = 822163944U;
        static const AkUniqueID SS_AIR_SIZE = 3074696722U;
        static const AkUniqueID SS_AIR_STORM = 3715662592U;
        static const AkUniqueID SS_AIR_TIMEOFDAY = 3203397129U;
        static const AkUniqueID SS_AIR_TURBULENCE = 4160247818U;
    } // namespace GAME_PARAMETERS

    namespace TRIGGERS
    {
        static const AkUniqueID KEY_PICKUP = 761761105U;
        static const AkUniqueID LIFE_CHANGE = 3732141486U;
        static const AkUniqueID PUZZLE_SUCCESS = 2687737065U;
    } // namespace TRIGGERS

    namespace BANKS
    {
        static const AkUniqueID INIT = 1355168291U;
        static const AkUniqueID MUSIC = 3991942870U;
    } // namespace BANKS

    namespace BUSSES
    {
        static const AkUniqueID MASTER_AUDIO_BUS = 3803692087U;
    } // namespace BUSSES

    namespace AUDIO_DEVICES
    {
        static const AkUniqueID NO_OUTPUT = 2317455096U;
        static const AkUniqueID SYSTEM = 3859886410U;
    } // namespace AUDIO_DEVICES

}// namespace AK

#endif // __WWISE_IDS_H__
