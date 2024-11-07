namespace Minesweeper
{
    public sealed class ControlDisposingEventArgs<T> : EventArgs
        where T : Control
    {
        public ControlDisposingEventArgs(T control)
        {
            Control = control;
        }

        public T Control { get; }
    }
}
