using System;
using Numeral.Internals;

namespace Numeral
{
    using BoolBinop = BinaryOperation<bool, bool, bool>;
    using BoolReducer = ReductionOperation<bool, bool>;
    using BoolUnop = UnaryOperation<bool, bool>;

    public static partial class Tensor
    {
        private static readonly BoolReducer s_all = AsReduceOp<bool, bool>((x, acc) => x && acc, true);
        private static readonly BoolReducer s_any = AsReduceOp<bool, bool>((x, acc) => x || acc, false);
        private static readonly BoolBinop s_and = AsBinaryOp<bool, bool, bool>((x, acc) => x && acc);
        private static readonly BoolBinop s_or = AsBinaryOp<bool, bool, bool>((x, acc) => x || acc);
        private static readonly BoolUnop s_not = AsUnaryOp<bool, bool>(x => !x);

        public static bool All(this Tensor<bool> tensor) => s_all.Call(tensor);

        public static Tensor<bool> All(this Tensor<bool> tensor, int axis = -1)
            => s_all.Call(tensor, axis);

        public static bool Any(this Tensor<bool> tensor) => s_any.Call(tensor);

        public static Tensor<bool> Any(this Tensor<bool> tensor, int axis = -1)
            => s_any.Call(tensor, axis);

        public static Tensor<bool> And(this Tensor<bool> x, Tensor<bool> y)
            => s_and.Call(x, y);

        public static Tensor<bool> Or(this Tensor<bool> x, Tensor<bool> y)
            => s_or.Call(x, y);

        public static Tensor<bool> Not(this Tensor<bool> x)
            => s_not.Call(x);
    }
}
