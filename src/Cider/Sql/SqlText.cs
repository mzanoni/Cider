using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cider.Sql;

public class SqlText
{
    private readonly List<Condition> _conditions = new();

    public SqlText IfWhere(bool predicate, string term)
    {
        return predicate
            ? Where(term)
            : this;
    }

    public SqlText IfNotWhere(bool predicate, string term) => IfWhere(!predicate, term);

    public SqlText Where(string term)
    {
        if (!string.IsNullOrWhiteSpace(term))
            _conditions.Add(new Condition(term));

        return this;
    }

    public SqlText Where(Func<SqlText, SqlText> subWhere)
    {
        string sql = subWhere(Empty).BuildConditions();

        _conditions.Add(new Condition(sql));

        return this;
    }

    public SqlText OrWhere(string term)
    {
        if (!string.IsNullOrWhiteSpace(term))
            _conditions.Add(new Condition(term, isOr: true));

        return this;
    }

    public string BuildConditions()
    {
        if (!_conditions.Any())
        {
            return string.Empty;
        }

        var builder = new StringBuilder();

        builder.Append($"({_conditions[0].Criteria})");

        foreach (Condition condition in _conditions.Skip(1))
        {
            builder.Append(condition.IsOr ? " OR " : " AND ");

            builder.Append($"({condition.Criteria})");
        }

        return builder.ToString();
    }

    public static SqlText Empty => new();

    public override string ToString()
    {
        var sqlWhere = BuildConditions();

        if (sqlWhere != string.Empty)
            sqlWhere = $"WHERE {sqlWhere}";

        return sqlWhere;
    }

    public static implicit operator string(SqlText where)
    {
        return where.ToString();
    }

    private class Condition
    {
        public Condition(string criteria, bool isOr = false)
        {
            Criteria = criteria;
            IsOr = isOr;
        }

        public string Criteria { get; }
        public bool IsOr { get; }
    }
}