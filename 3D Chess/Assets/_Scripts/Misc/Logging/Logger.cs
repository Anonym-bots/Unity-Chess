using System;
using UnityEngine;
using System.Collections.Generic;
using Object = UnityEngine.Object;


// WRITTEN BY OLE GEIßLER
// Made for ease of use for debugs. Better overview, filter options, etc.
// the enums are for filtering by category
public enum LogType
{
    None,
    Bug,
    Missing,
    Todo,
    Observing   // use for currently observing stuff
}

public enum LoggerClass
{
    None,       // Minor things
    System,     // Save/Load, Settings, Multiplayer, etc.
    Game,       // GameStat, Menu or smth
    Player,     // Movement, Health, etc.
    Entity,     // Enemies, mobs, etc.
    Shooting,   // Weapons, Projectiles
    Environment,// Pickups, etc.
    Audio       // AudioClips, Volume etc.
}


[AddComponentMenu("_MyAssets/Services/logging")]
public class Logger : Singleton<Logger>
{
    [Space(20)]
    // store one of the categories to focus and filter all the others
    [SerializeField] private LogType focusLogType;
    [SerializeField] private LoggerClass focusLogger;
    [Space(40)]

    [SerializeField] private List<DisplayLogType> displayLogTypes;  // store all types
    [SerializeField] private List<LogClass> loggers;    // same as above

    [System.Serializable]
    class DisplayLogType
    {
        // one for each value in enum
        // bool for whether to display debugs of this kind
        public bool display;
        public LogType logType;

        public DisplayLogType(LogType logType, bool display = true)
        {
            this.logType = logType;
            this.display = display;
        }
    }

    [System.Serializable]
    class LogClass
    {
        // one for each value in enum
        // bool for whether to display debugs of this kind
        public LoggerClass loggerClass;
        public bool showLogs;
        public string prefix;   // make the have a prefix
        public Color prefixColor;   // color the prefix

        public LogClass(LoggerClass loggerClass, string prefix = null, bool showLogs = true)
        {
            this.loggerClass = loggerClass;
            if (prefix == null) this.prefix = loggerClass.ToString();
            else this.prefix = prefix;
            this.showLogs = showLogs;
        }
    }

    // store current color as hexCode
    private string hexColor;


    protected override void Awake()
    {
        base.Awake();
        dontDestroyOnLoad = true;
    }

    // make sure one of each enum value exists in the lists
    private void OnValidate()
    {
        int i = -1;
        //* Fill LogType list
        foreach (LogType type in (LogType[])Enum.GetValues(typeof(LogType)))
        {
            i++;

            // dont want to replace those already existing as they would lose color, prefix, etc.
            #region Error check
            bool logAlreadyExisting = false;
            foreach (DisplayLogType log in displayLogTypes)
            {
                if (log.logType == type)
                {
                    logAlreadyExisting = true;
                }
            }
            if (logAlreadyExisting) continue;   // skip this iteration
            #endregion

            // if not existent, create new DisplayLogType
            DisplayLogType lt = new DisplayLogType(type, true);
            displayLogTypes.Insert(i, lt);
        }

        // same as above for the other list
        //* Fill loggers list
        i = -1;
        foreach (LoggerClass loggerClass in (LoggerClass[])Enum.GetValues(typeof(LoggerClass)))
        {
            i++;

            #region Error check
            bool logAlreadyExisting = false;
            foreach (LogClass log in loggers)
            {
                if (log.loggerClass == loggerClass)
                {
                    logAlreadyExisting = true;
                }
            }
            if (logAlreadyExisting) continue;
            #endregion

            LogClass lc = new LogClass(loggerClass);
            lc.prefixColor.a = 1f;  // make sure the color has an alpha of 1
            loggers.Insert(i, lc);
        }

        /*  use only if the prefix should always equal the name in enum!
        foreach (LogClass logClass in loggers)
        {
            if (logClass.loggerClass == LoggerClass.None)
            {
                logClass.prefix = "";
                continue;
            }
            logClass.prefix = logClass.loggerClass.ToString();
        }
        */
    }

    // method to call from other scripts
    public void Log(string message, Object sender, LoggerClass lc = LoggerClass.None, LogType type = LogType.None)
    {
        // store LogClass and DisplayLogType
        LogClass logClass = GetLoggerFromEnum(lc);
        DisplayLogType dlt = GetDisplayLogTypeFromEnum(type);



        #region Focus / Display filter
        bool loggerFocused = focusLogger != LoggerClass.None;
        bool logTypeFocused = focusLogType != LogType.None;
        bool isFocusedLogger = loggerFocused && focusLogger == logClass.loggerClass;
        bool isFocusedLogType = loggerFocused && focusLogType == dlt.logType;

        if (!isFocusedLogger && loggerFocused || !isFocusedLogType && logTypeFocused) return;    // either has another focus so return

        // if neither focused nor displayed -> return
        if (!isFocusedLogger && !logClass.showLogs) return;
        if (!isFocusedLogType && !dlt.display) return;
        #endregion



        // get hexcode for prefix color
        hexColor = "#" + ColorUtility.ToHtmlStringRGBA(logClass.prefixColor);
        string result;

        // display prefix
        if (type == LogType.None) result = $"<b><color={hexColor}>{logClass.prefix}";
        else result = $"<b><color={hexColor}>{logClass.prefix} | ";
        result += "</color><color=";

        switch (type)   // get color according to type of debug
        {
            case LogType.Bug:
                result += "red";
                break;
            case LogType.Missing:
                result += "yellow";
                break;
            case LogType.Todo:
                result += "orange";
                break;
            case LogType.Observing:
                result += "purple";
                break;
            default:
                break;
        }

        result += ">";
        if (type != LogType.None) result += type.ToString().ToUpper();

        result += "</color> </b>\n ";
        if (sender != null) result += sender.name;
        result += " | <color=white>" + message + "</color>";

        // Debug the result of all this
        if (sender != null) Debug.Log(result, sender);
        else Debug.Log(result);
    }


    // search for the according object in the list
    private LogClass GetLoggerFromEnum(LoggerClass loggerClass)
    {
        foreach (LogClass lc in loggers)
        {
            if (lc.loggerClass == loggerClass)
            {
                return lc;
            }
        }
        return loggers[0];
    }

    // search for the according object in the list
    private DisplayLogType GetDisplayLogTypeFromEnum(LogType type)
    {
        foreach (DisplayLogType dlt in displayLogTypes)
        {
            if (dlt.logType == type)
            {
                return dlt;
            }
        }
        return displayLogTypes[0];
    }
}