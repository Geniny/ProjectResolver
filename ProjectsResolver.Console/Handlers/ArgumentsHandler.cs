using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsResolver.Console.Handlers
{
    public class ArgumentsHandler
    {
        public List<string> SplitProperties(string properties)
        {
            var splitedProperties = new List<string>();
            var isSplitting = true;

            if (string.IsNullOrEmpty(properties))
                return null;

            var splittingProps = this.SpaceSplit(properties);
            while (isSplitting)
            {
                var leftPart = string.Empty;
                var rightPart = string.Empty;
                if (splittingProps.Length <= 0)
                {
                    isSplitting = false;
                    continue;
                }
                else if (splittingProps.Length > 1)
                {
                    leftPart = splittingProps[0];
                    rightPart = splittingProps[1];
                }
                else
                {
                    leftPart = splittingProps[0];
                }

                if (string.IsNullOrEmpty(leftPart))
                {
                    isSplitting = false;
                }
                else
                {
                    if (IsSingleProperty(leftPart))
                    {
                        splitedProperties.Add(leftPart);
                        if (!string.IsNullOrEmpty(rightPart))
                            splittingProps = this.SpaceSplit(rightPart);
                        else
                            isSplitting = false;

                        continue;
                    }
                    else if (IsPairedProperty(leftPart))
                    {
                        if (!string.IsNullOrEmpty(rightPart))
                        {
                            splittingProps = this.SpaceSplit(splittingProps[1]);
                            if (!string.IsNullOrEmpty(splittingProps[0]))
                            {
                                splitedProperties.Add(leftPart + " " + splittingProps[0]);
                                if (splittingProps.Length > 1)
                                    splittingProps = this.SpaceSplit(splittingProps[1]);
                                else
                                    isSplitting = false;
                            }
                        }
                        else
                        {
                            throw new ArgumentNullException(
                                $"Can't parse {leftPart} without ",
                                nameof(rightPart)
                            );
                        }
                    }
                    else if (IsValueProperty(leftPart))
                    {
                        splitedProperties.Add(leftPart);
                        if (splittingProps.Length > 1 && !string.IsNullOrEmpty(rightPart))
                        {
                            splittingProps = this.SpaceSplit(rightPart);
                        }
                        else
                        {
                            isSplitting = false;
                            continue;
                        }
                    }
                }
            }

            return splitedProperties;
        }

        public bool IsSingleProperty(string property)
        {
            return property.StartsWith("--");
        }

        public bool IsPairedProperty(string property)
        {
            return property.StartsWith("-");
        }

        public bool IsValueProperty(string property)
        {
            return !property.StartsWith("-") && !property.Contains("-");
        }

        private string[] SpaceSplit(string properties)
        {
            return properties.Trim().Split(' ', 2);
        }
    }
}
