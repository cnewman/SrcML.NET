﻿/******************************************************************************
 * Copyright (c) 2013 ABB Group
 * All rights reserved. This program and the accompanying materials
 * are made available under the terms of the Eclipse Public License v1.0
 * which accompanies this distribution, and is available at
 * http://www.eclipse.org/legal/epl-v10.html
 *
 * Contributors:
 *    Vinay Augustine (ABB Group) - initial API, implementation, & documentation
 *****************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ABB.SrcML.Data {
    /// <summary>
    /// The variable use class represents a use of a variable.
    /// </summary>
    public class VariableUse : AbstractUse<VariableDeclaration>, IResolvesToType {

        /// <summary>
        /// Searches through the <see cref="Scope.DeclaredVariables"/> to see if any of them <see cref="Matches(VariableDeclaration)">matches</see>
        /// </summary>
        /// <returns>An enumerable of matching variable declarations.</returns>
        public override IEnumerable<VariableDeclaration> FindMatches() {
            var currentScope = this.ParentScope;

            var matchingVariables = from scope in ParentScopes
                                    from variable in scope.DeclaredVariables
                                    where Matches(variable)
                                    select variable;
            return matchingVariables;
        }

        /// <summary>
        /// Tests if this variable usage is a match for <paramref name="definition"/>
        /// </summary>
        /// <param name="definition">The variable declaration to test</param>
        /// <returns>true if this matches the variable declaration; false otherwise</returns>
        public override bool Matches(VariableDeclaration definition) {
            return definition != null && definition.Name == this.Name;
        }

        /// <summary>
        /// Finds all of the matching type definitions for all of the variable declarations that match this variable use
        /// </summary>
        /// <returns>An enumerable of matching type definitions</returns>
        public IEnumerable<TypeDefinition> FindMatchingTypes() {
            var typeDefinitions = from declaration in FindMatches()
                                  where declaration.VariableType != null
                                  from typeDefinition in declaration.VariableType.FindMatches()
                                  select typeDefinition;
            return typeDefinitions;
        }

        public TypeDefinition FindFirstMatchingType() {
            return FindMatchingTypes().FirstOrDefault();
        }
    }
}
