using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace IoCTest.Helpers
{
    /// <summary>
    /// Keys built to correlate linelist entries with specific DWG files
    /// <para>Strategy will typically be to use filename per DWG to mitigate time used...
    /// Yet it may still require opening drawing and grabbing floating text, attributes, etc.</para>
    /// </summary>
    public static class KeyBuilder
    {
        // TODO: Change this Regex found key builder into a definitive structure decoupled from formatting per-key...
        // TODO: Support pulling attribute from dwg to supplement composite...need structured requirements to handle process 

        /// <summary>
        /// <para>
        /// Given a set of regular expressions as well as a set of imperative keys, this will attempt a pattern match
        /// based on each one.  As soon as a valid match is found, the entire constructed key is returned.</para>
        /// <para>Renamed to build dumb key because does not include validation settings (per-key/overall), per-key formatting,
        /// or itemization of each of these matches returned (may use dictionary (key -> found value) instead of string)
        /// </para>
        /// <para>
        /// Construct a composite key from a series of reg expressions and a string input.
        /// If a "valid" non-empty key is found then flag the state as such.
        /// </para>
        /// </summary>
        /// <param name="input"></param>
        /// <param name="validExprList"></param>
        /// <param name="neededKeys"></param>
        /// <param name="foundKey"></param>
        /// <param name="keyDelim"></param>
        /// <returns></returns>
        public static string BuildDumbCompositeKey(string input, IList<Regex> validExprList, IList<string> neededKeys,
            out bool foundKey, string keyDelim = "", bool allowEmpties = false)
        {
            string key = string.Empty;
            // Only flag as found immediately before return in success state section, otherwise we never found anything
            foundKey = false;

            if (!String.IsNullOrEmpty(input))
            {
                // Use any provided expressions to adopt matches
                foreach (Regex findExpr in validExprList)
                {
                    Match keyMatch = findExpr.Match(input);

                    // If THIS Regex yielded a match, construct the key
                    // Wont: decide what formats to support
                    if (keyMatch.Success)
                    {
                        // Reset the key build
                        key = string.Empty;

                        // Retrieve all group names based on this expression
                        IList<string> groupNames = findExpr.GetGroupNames();
                        GroupCollection groups = keyMatch.Groups;

                        // Walk down required key list, append to current key
                        foreach (string neededKey in neededKeys)
                        {
                            // Only attempt to grab required key if present in found groups
                            if (groupNames.Contains(neededKey))
                            {
                                // APPEND PARTIAL KEY W/DELIM
                                //If group/key is considered part of the composite key, append
                                // Done: TODO: See what key/s to provide in config. Should be ordered/case insensitive
                                // If the particular key is empty and we aren't allowing that, fail out with error
                                if (!allowEmpties && groups[neededKey].Value == string.Empty)
                                {
                                    throw new System.Exception
                                    ("File: " + input + "\n" +
                                     "Can't find key: " + neededKey +
                                     " based on expression:\n" + findExpr);
                                }
                                else // We don't care if it's empty, append and move on!
                                {
                                    string singleKey = groups[neededKey].Value;
                                    key += singleKey;
                                    key += keyDelim;
                                }
                            }
                            else
                                throw new System.Exception
                                ("Required Key {" + neededKey +
                                 "} not found in expression's provided groups\n" +
                                 String.Join("\n", Enumerable.ToArray(groupNames)));
                        }
                    }

                    // TODO: STRATEGY: See if key was composed of ALL necessary ones
                    // Key delims are present here, need to ignore them actively for test
                    // We would have exploded here w/out finding the key, assume this is successful for now...
                    if (key != string.Empty)
                    {
                        foundKey = true;
                        return key.TrimEnd(keyDelim.ToCharArray());
                    }
                }
            }

            return key;
        }

    }
}