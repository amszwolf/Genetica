﻿// ------------------------------------------
// <copyright file="PrefixNotationEditSimilarity.cs" company="Pedro Sequeira">
// 
//     Copyright (c) 2018 Pedro Sequeira
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated
// documentation files (the "Software"), to deal in the Software without restriction, including without limitation the
// rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to the following conditions:
//  
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the
// Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS
// OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// 
// </copyright>
// <summary>
//    Project: Genesis
//    Last updated: 03/31/2018
//    Author: Pedro Sequeira
//    E-mail: pedrodbs@gmail.com
// </summary>
// ------------------------------------------

using System;
using Genesis.Elements;
using Genesis.Util;

namespace Genesis.Similarity
{
    /// <summary>
    ///     Measures the similarity of two <see cref="MathProgram" /> based on the edit or Levenshtein string distance between
    ///     the expressions of the programs in prefix notation, i.e., based on the number of edits needed to transform the
    ///     expression of one program into the one of the other program.
    /// </summary>
    public class PrefixNotationEditSimilarity : ISimilarityMeasure<MathProgram>
    {
        #region Public Methods

        /// <inheritdoc />
        public double Calculate(MathProgram prog1, MathProgram prog2)
        {
            if (prog1 == null || prog2 == null) return 0;
            if (prog1.Equals(prog2)) return 1;

            var expr1 = MathExpressionConverter.ToPrefixNotationExpression(prog1);
            var expr2 = MathExpressionConverter.ToPrefixNotationExpression(prog2);
            return 1d - (double) expr1.LevenshteinDistance(expr2) / Math.Max(expr1.Length, expr2.Length);
        }

        #endregion
    }
}