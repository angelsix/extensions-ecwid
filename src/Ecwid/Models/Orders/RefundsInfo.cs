// Licensed under the MIT License. See LICENSE in the git repository root for license information.

using Newtonsoft.Json;
using System;

namespace Ecwid.Models
{
    /// <summary>
    /// Information about refunds for an order
    /// </summary>
    public class RefundsInfo
    {
        /// <summary>
        /// Gets or sets the date/time of a refund, e.g 2014-06-06 18:57:19 +0000
        /// </summary>
        /// <value>
        /// The date
        /// </value>
        [JsonProperty("date")]
        public DateTimeOffset Date { get; set; }

        /// <summary>
        /// Gets or sets what action triggered refund. Possible values: 
        /// <list type="table">
        /// <item><term>CP</term><description>changed my merchant in Ecwid CP</description></item>
        /// <item><term>API</term><description>changed by another app</description></item>
        /// <item><term>External</term><description>refund made from payment processor website</description></item>
        /// </list>
        /// </summary>
        /// <value>
        /// The date
        /// </value>
        [JsonProperty("source")]
        public string Source { get; set; }

        /// <summary>
        /// Gets or sets the reason for a refund. 256 characters max
        /// </summary>
        /// <value>
        /// The reason
        /// </value>
        [JsonProperty("reason")]
        public string Reason { get; set; }

        /// <summary>
        /// Gets or sets the amount of this specific refund 
        /// (not total amount refunded for order. see <see cref="OrderEntry.RefundedAmount"/> field)
        /// </summary>
        /// <value>
        /// The date
        /// </value>
        [JsonProperty("amount")]
        public double Amount { get; set; }
    }
}
