using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImplementationsLibrary
{
    public interface IScalarSATable
    {
        IEnumerable<Line> GetProjectionLines();

        (float min, float max) ProjectOntoLineScalar(Line line);

        bool SeparatingAxisTheoremScalar(IScalarSATable other);
    }

    public interface ISATable
    {
        IEnumerable<Line> GetProjectionLines();

        Line ProjectOntoLine(Line line);

        bool SeparatingAxisTheorem(ISATable other);
    }

    public static class SATHelper
    {
        public static bool SeparatingAxisTheoremScalar(IScalarSATable object1, IScalarSATable object2)
        {
            IEnumerable<Line> projectionLines = object1.GetProjectionLines().Concat(object2.GetProjectionLines());

            foreach (Line line in projectionLines)
            {
                (float min, float max) first = object1.ProjectOntoLineScalar(line);
                (float min, float max) second = object2.ProjectOntoLineScalar(line);

                if (first.min > second.min)
                {
                    var temp = first;
                    first = second;
                    second = temp;
                }

                if (first.max < second.min)
                {
                    return false;
                }
            }

            return true;
        }
        public static bool SeparatingAxisTheorem(ISATable object1, ISATable object2)
        {
            IEnumerable<Line> projectionLines = object1.GetProjectionLines().Concat(object2.GetProjectionLines());

            foreach (Line line in projectionLines)
            {
                Line first = object1.ProjectOntoLine(line);
                Line second = object2.ProjectOntoLine(line);

                if (!first.Intersects(second))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
