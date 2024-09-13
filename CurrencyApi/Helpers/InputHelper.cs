namespace CurrencyApi.Helpers
{
    /// <summary>
    /// The input helper.
    /// </summary>
    public class InputHelper
    {
        /// <summary>
        /// Check input value.
        /// </summary>
        /// <param name="inputType">The input type.</param>
        /// <param name="value">The value.</param>
        /// <returns>A (bool,string)</returns>
        public static (bool,string) CheckInputValue(InputType inputType, string value)
        {
            bool result = false; 
            switch(inputType)
            {
                case InputType.CurrencyCode:
                    result = value.Length == 3;
                    if (!result)
                        return (false, "Please enter valid Currency Code (3 letters)");
                    break;
                case InputType.Amount:
                    result = double.TryParse(value, out var amount) || value == "0";
                    if (!result)
                        return (false, "Please enter valid amount");
                    break;
                    default: return (false, "no input by user, please enter correct values");
            }
            return (result, "Please enter valid input");
        }
    }

    /// <summary>
    /// The input types.
    /// </summary>
    public enum InputType { CurrencyCode, Amount}
}
