namespace OracleCommunication_Demo
{
    public enum DemoType
    {
        BasicGuidance,
        RemoteSupport,
        Personal_Shopper,
        Concierge,
        Collaboration
    }

    public enum DialogType
    {
        Chat,
        Audio,
        AllowControl,
        StopControl,
        StartVideo,
        StopService,
        Meeting,
        StopUserSharing,
        StartRecording
    }

    public enum DialogMode
    {
        Normal,
        SingleAction
    }

    public enum ChatType
    {
        Incoming,
        Outgoing
    }

    public enum VideoFallBackType
    {
        None,
        Reconnecting,
        Reconnectionfailed
    }

    public enum AudioStates
    {
        Stop,
        Active,
        Deactive
    }

    public enum WebcamStates
    {
        Normal,
        Demo
    }

    public enum CollaborationStates
    {
        VideoExpanded,
        AgentExpanded
    }
}
