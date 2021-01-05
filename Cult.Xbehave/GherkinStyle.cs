// ReSharper disable All 
namespace Xbehave
{
    /// <summary>
    /// A simple class to follow Gherkin style.
    /// </summary>
    public static class GherkinStyle
    {
        /// <summary>
        /// The And helps to use behaviors together.
        /// </summary>
        /// <param name="text">The step text.</param>
        /// <param name="prefix">The prefix of the step text.</param>
        /// <param name="postfix">The postfix of the step text.</param>
        /// <returns>The And part as a string</returns>
        public static string And(string text, string prefix = "And", string postfix = "") => $"{prefix} {text} {postfix}";
        /// <summary>
        /// The Given part describes the state of the world before you begin the behavior you're specifying in this scenario.
        /// </summary>
        /// <param name="text">The step text.</param>
        /// <param name="prefix">The prefix of the step text.</param>
        /// <param name="postfix">The postfix of the step text.</param>
        /// <returns>The Given part as a string</returns>
        public static string Given(string text, string prefix = "Given", string postfix = "") => $"{prefix} {text} {postfix}";
        /// <summary>
        ///  The Then section describes the changes you expect due to the specified behavior. 
        /// </summary>
        /// <param name="text">The step text.</param>
        /// <param name="prefix">The prefix of the step text.</param>
        /// <param name="postfix">The postfix of the step text.</param>
        /// <returns>The Then part as a string</returns>
        public static string Then(string text, string prefix = "Then", string postfix = "") => $"{prefix} {text} {postfix}";
        /// <summary>
        /// The When section is that behavior that you're specifying.
        /// </summary>
        /// <param name="text">The step text.</param>
        /// <param name="prefix">The prefix of the step text.</param>
        /// <param name="postfix">The postfix of the step text.</param>
        /// <returns>The When part as a string</returns>
        public static string When(string text, string prefix = "When", string postfix = "") => $"{prefix} {text} {postfix}";
    }
}
