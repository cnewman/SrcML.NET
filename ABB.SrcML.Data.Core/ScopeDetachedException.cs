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

    public class ScopeDetachedException : Exception {
        public IScope DetachedScope;

        public ScopeDetachedException(IScope detachedScope)
            : this(detachedScope, String.Format("{0} is not attached to a global scope", detachedScope.Id)) {
        }

        public ScopeDetachedException(IScope detachedScope, string message)
            : base(message) {
            this.DetachedScope = detachedScope;
        }
    }
}