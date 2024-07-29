public class DangerMomentNotifier
{
    public event Action OnDangerMomentStarted;
    public event Action OnDangerMomentEnded;

    bool isDangerMomentNow = false;
    HashSet<string> dangerSignals = new HashSet<string>();

    HashSet<string> startSignals = new HashSet<string>() { "Corner", "Dangerous attack", "Card" };
    HashSet<string> endSignals = new HashSet<string>() { "Corner ended", "Dangerous attack ended", "Card ended" };
    public void GetSignal(string signal)
    {
        if (startSignals.Contains(signal))
        {
            dangerSignals.Add(signal);
            if (!isDangerMomentNow)
            {
                isDangerMomentNow = true;
                OnDangerMomentStarted?.Invoke();
            }
        }
        else
            if (endSignals.Contains(signal))
        {
            dangerSignals.Remove(InvertSignal(signal));
            if (dangerSignals.Count == 0 && isDangerMomentNow)
            {
                isDangerMomentNow = false;
                OnDangerMomentEnded?.Invoke();
            }
        }
    }

    string InvertSignal(string input)
    {
        switch (input)
        {
            case "Corner ended":
                return "Corner";
            case "Dangerous attack ended":
                return "Dangerous attack";
            case "Card ended":
                return "Card";
            default: return "";
        }
    }
}
