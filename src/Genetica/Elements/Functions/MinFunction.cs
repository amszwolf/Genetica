// ------------------------------------------
// <copyright file="MinFunction.cs" company="Pedro Sequeira">
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
//    Project: Genetica
//    Last updated: 03/31/2018
//    Author: Pedro Sequeira
//    E-mail: pedrodbs@gmail.com
// </summary>
// ------------------------------------------

using System;
using System.Collections.Generic;
using Genetica.Elements.Terminals;

namespace Genetica.Elements.Functions
{
    /// <summary>
    ///     Represents a <see cref="CommutativeBinaryFunction" /> selecting the minimum between two sub-programs.
    /// </summary>
    public class MinFunction : CommutativeBinaryFunction
    {
        #region Constructors

        /// <summary>
        ///     Creates a new <see cref="MinFunction" />.
        /// </summary>
        /// <param name="firstProgram">The first sub-program.</param>
        /// <param name="secondProgram">The second sub-program.</param>
        public MinFunction(ITreeProgram<double> firstProgram, ITreeProgram<double> secondProgram) :
            base(firstProgram, secondProgram)
        {
        }

        #endregion

        #region Properties & Indexers

        /// <inheritdoc />
        public override string Label => "min";

        #endregion

        #region Public Methods

        /// <inheritdoc />
        public override double Compute() => Math.Min(this.FirstParameter.Compute(), this.SecondParameter.Compute());

        /// <inheritdoc />
        public override ITreeProgram<double> CreateNew(IList<ITreeProgram<double>> children) =>
            children == null || children.Count != 2
                ? null
                : new MinFunction(children[0], children[1]);

        /// <inheritdoc />
        public override ITreeProgram<double> Simplify()
        {
            // if its a constant value, just return a constant with that value
            if (this.IsConstant())
                return new Constant(this.Compute());

            // otherwise first tries to simplify children
            var input = new ITreeProgram<double>[this.Input.Count];
            for (var i = 0; i < this.Input.Count; i++)
                input[i] = this.Input[i].Simplify();

            // if operands are equal return the first
            if (input[0].Equals(input[1])) return input[0];

            // check whether one of operands is maximum and return the other
            if (input[0].EqualsConstant(double.MaxValue) || input[0].EqualsConstant(double.PositiveInfinity))
                return input[1];
            if (input[1].EqualsConstant(double.MaxValue) || input[1].EqualsConstant(double.PositiveInfinity))
                return input[0];

            return this.CreateNew(input);
        }

        #endregion
    }
}