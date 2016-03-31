using System;

namespace Ecwid.Test.Services.Legacy
{
    internal static class Tools
    {
        /// <summary>
        /// Timeses the specified action.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <param name="action">The action.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static void Times(this int count, Action action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));
            if (count <= 0)
                throw new ArgumentException(nameof(count));
            for (var i = 0; i < count; i++)
                action();
        }
    }
}