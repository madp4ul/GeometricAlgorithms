using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Domain.Drawables;
using GeometricAlgorithms.Domain.Trees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.BusinessLogic.Model
{
    public class TreeEnumerationModel : IHasDrawables, IUpdatable<IEnumerableTree>
    {
        private readonly IDrawableFactoryProvider DrawableFactoryProvider;
        private readonly IRefreshableView RefreshableView;

        private readonly ContainerDrawable CurrentDrawable;

        private ITreeEnumerator TreeEnumerator;

        public string CurrentName => TreeEnumerator.Current?.ToString();
        public bool HasEnumerator => TreeEnumerator != null;

        public TreeEnumerationModel(IDrawableFactoryProvider drawableFactoryProvider, IRefreshableView refreshableView)
        {
            DrawableFactoryProvider = drawableFactoryProvider ?? throw new ArgumentNullException(nameof(drawableFactoryProvider));
            RefreshableView = refreshableView ?? throw new ArgumentNullException(nameof(refreshableView));

            CurrentDrawable = new ContainerDrawable(enable: true);
        }

        public event Action Updated;

        public bool CanSelectParent => TreeEnumerator?.Current?.HasParent ?? false;
        public int ChildCount => TreeEnumerator?.Current?.ChildCount ?? 0;

        public void Update(IEnumerableTree tree)
        {
            TreeEnumerator = tree.GetTreeEnumerator();
            CurrentDrawable.SwapDrawable(new EmptyDrawable());

            Updated?.Invoke();
        }

        public void SelectRoot()
        {
            TreeEnumerator.MoveToRoot();

            SwapDrawable(TreeEnumerator.Current);
        }

        public void SelectChild(int childIndex)
        {
            if (childIndex >= ChildCount)
            {
                throw new InvalidOperationException();
            }

            TreeEnumerator.MoveToChild(childIndex);

            SwapDrawable(TreeEnumerator.Current);
        }

        public void SelectParent()
        {
            if (!CanSelectParent)
            {
                throw new InvalidOperationException();
            }

            TreeEnumerator.MoveToParent();
            SwapDrawable(TreeEnumerator.Current);
        }

        private void SwapDrawable(ITreeNode node)
        {
            IDrawable drawable;
            if (node != null)
            {
                drawable = DrawableFactoryProvider.DrawableFactory.CreateBoundingBoxRepresentation(new[] { node.BoundingBox }, b => Vector3.One);
            }
            else
            {
                drawable = new EmptyDrawable();
            }

            CurrentDrawable.SwapDrawable(drawable);

            RefreshableView.Refresh();
        }

        public IEnumerable<IDrawable> GetDrawables()
        {
            yield return CurrentDrawable;
        }
    }
}
