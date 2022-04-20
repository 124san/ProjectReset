using System.Linq;

public class ConditionHelper {
    public static bool CheckCondition(bool checkAll, Condition[] conditions) {
        if (checkAll) {
            return conditions.Length == 0 || conditions.All(x => x.CheckCondition());
        }
        else {
            return conditions.Length == 0 || conditions.Any(x => x.CheckCondition());
        }
    }
}