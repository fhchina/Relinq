// This file is part of the re-motion Core Framework (www.re-motion.org)
// Copyright (C) 2005-2009 rubicon informationstechnologie gmbh, www.rubicon.eu
// 
// The re-motion Core Framework is free software; you can redistribute it 
// and/or modify it under the terms of the GNU Lesser General Public License 
// version 3.0 as published by the Free Software Foundation.
// 
// re-motion is distributed in the hope that it will be useful, 
// but WITHOUT ANY WARRANTY; without even the implied warranty of 
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the 
// GNU Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public License
// along with re-motion; if not, see http://www.gnu.org/licenses.
// 
using System;
using System.Linq.Expressions;
using Remotion.Data.Linq.Clauses.Expressions;
using Remotion.Data.Linq.Clauses.ExpressionTreeVisitors;
using Remotion.Utilities;

namespace Remotion.Data.Linq.Clauses
{
  /// <summary>
  /// Represents the main data source in a query, producing data items that are filtered, aggregated, projected, or otherwise processed by
  /// subsequent clauses.
  /// </summary>
  /// <example>
  /// In C#, the first "from" clause in the following sample corresponds to the <see cref="MainFromClause"/>:
  /// <ode>
  /// var query = from s in Students
  ///             from f in s.Friends
  ///             select f;
  /// </ode>
  /// </example>
  public class MainFromClause : FromClauseBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="MainFromClause"/> class.
    /// </summary>
    /// <param name="itemName">A name describing the items generated by the from clause.</param>
    /// <param name="itemType">The type of the items generated by the from clause.</param>
    /// <param name="fromExpression">The <see cref="Expression"/> generating data items for this from clause.</param>
    public MainFromClause (string itemName, Type itemType, Expression fromExpression)
        : base (
            ArgumentUtility.CheckNotNullOrEmpty ("itemName", itemName),
            ArgumentUtility.CheckNotNull ("itemType", itemType),
            ArgumentUtility.CheckNotNull ("fromExpression", fromExpression))
    {
    }

    /// <summary>
    /// Accepts the specified visitor by calling its <see cref="IQueryModelVisitor.VisitMainFromClause"/> method.
    /// </summary>
    /// <param name="visitor">The visitor to accept.</param>
    /// <param name="queryModel">The query model in whose context this clause is visited.</param>
    public virtual void Accept (IQueryModelVisitor visitor, QueryModel queryModel)
    {
      ArgumentUtility.CheckNotNull ("visitor", visitor);
      ArgumentUtility.CheckNotNull ("queryModel", queryModel);

      visitor.VisitMainFromClause (this, queryModel);
    }

    /// <summary>
    /// Clones this clause, registering its clone with the <paramref name="cloneContext"/>.
    /// </summary>
    /// <param name="cloneContext">The clones of all query source clauses are registered with this <see cref="CloneContext"/>.</param>
    /// <returns>A clone of this clause.</returns>
    public MainFromClause Clone (CloneContext cloneContext)
    {
      ArgumentUtility.CheckNotNull ("cloneContext", cloneContext);

      var clone = new MainFromClause (ItemName, ItemType, FromExpression);
      cloneContext.ClauseMapping.AddMapping (this, new QuerySourceReferenceExpression (clone));
      clone.AddClonedJoinClauses (JoinClauses, cloneContext);
      return clone;
    }
  }
}