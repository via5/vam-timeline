using System;
using UnityEngine;
using UnityEngine.UI;

namespace VamTimeline
{
    /// <summary>
    /// VaM Timeline
    /// By Acidbubbles
    /// Animation timeline with keyframes
    /// Source: https://github.com/acidbubbles/vam-timeline
    /// </summary>
    public class AnimationControlPanel : MonoBehaviour
    {
        private DopeSheet _dopeSheet;

        public AnimationControlPanel()
        {
            gameObject.AddComponent<VerticalLayoutGroup>();
        }

        public void Bind(IAtomPlugin plugin)
        {
            // TODO: Integrate play/stop inside scrubber
            InitScrubber(plugin.ScrubberJSON, plugin.SnapJSON);
            InitSpacer();
            // TODO: Make the JSON use animation features instead of the other way around
            InitFrameNav(plugin.Manager.configurableButtonPrefab, plugin.PreviousFrameJSON, plugin.NextFrameJSON);
            InitSpacer();
            InitPlaybackButtons(plugin.Manager.configurableButtonPrefab, plugin.PlayJSON, plugin.StopJSON);
            InitSpacer();
            _dopeSheet = InitDopeSheet();
            InitSpacer();
        }

        private Scrubber InitScrubber(JSONStorableFloat scrubberJSON, JSONStorableFloat snapJSON)
        {
            var go = new GameObject();
            go.transform.SetParent(transform, false);

            go.AddComponent<LayoutElement>().preferredHeight = 60f;

            var scrubber = go.AddComponent<Scrubber>();
            scrubber.scrubberJSON = scrubberJSON;
            scrubber.snapJSON = snapJSON;

            return scrubber;
        }

        private void InitPlaybackButtons(Transform buttonPrefab, JSONStorableAction playJSON, JSONStorableAction stopJSON)
        {
            var container = new GameObject();
            container.transform.SetParent(transform, false);

            var gridLayout = container.AddComponent<HorizontalLayoutGroup>();
            gridLayout.spacing = 4f;
            gridLayout.childForceExpandWidth = false;
            gridLayout.childControlWidth = true;

            var play = Instantiate(buttonPrefab);
            play.SetParent(container.transform, false);
            play.GetComponent<UIDynamicButton>().label = "\u25B6 Play";
            play.GetComponent<UIDynamicButton>().button.onClick.AddListener(() => playJSON.actionCallback());
            play.GetComponent<LayoutElement>().preferredWidth = 0;
            play.GetComponent<LayoutElement>().flexibleWidth = 100;

            var stop = Instantiate(buttonPrefab);
            stop.SetParent(container.transform, false);
            stop.GetComponent<UIDynamicButton>().label = "\u25A0 Stop";
            stop.GetComponent<UIDynamicButton>().button.onClick.AddListener(() => stopJSON.actionCallback());
            stop.GetComponent<LayoutElement>().preferredWidth = 0;
            stop.GetComponent<LayoutElement>().flexibleWidth = 30;
        }

        private void InitFrameNav(Transform buttonPrefab, JSONStorableAction previousFrameJSON, JSONStorableAction nextFrameJSON)
        {
            var container = new GameObject();
            container.transform.SetParent(transform, false);

            var gridLayout = container.AddComponent<HorizontalLayoutGroup>();
            gridLayout.spacing = 4f;
            gridLayout.childForceExpandWidth = false;
            gridLayout.childControlWidth = true;

            var previousFrame = Instantiate(buttonPrefab);
            previousFrame.SetParent(container.transform, false);
            previousFrame.GetComponent<UIDynamicButton>().label = "\u2190 Previous Frame";
            previousFrame.GetComponent<UIDynamicButton>().button.onClick.AddListener(() => previousFrameJSON.actionCallback());
            previousFrame.GetComponent<LayoutElement>().preferredWidth = 0;
            previousFrame.GetComponent<LayoutElement>().flexibleWidth = 50;

            var nextFrame = Instantiate(buttonPrefab);
            nextFrame.SetParent(container.transform, false);
            nextFrame.GetComponent<UIDynamicButton>().label = "Next Frame \u2192";
            nextFrame.GetComponent<UIDynamicButton>().button.onClick.AddListener(() => nextFrameJSON.actionCallback());
            nextFrame.GetComponent<LayoutElement>().preferredWidth = 0;
            nextFrame.GetComponent<LayoutElement>().flexibleWidth = 50;
        }

        private DopeSheet InitDopeSheet()
        {
            var go = new GameObject();
            go.transform.SetParent(transform, false);

            go.AddComponent<LayoutElement>().flexibleHeight = 260f;

            var dopeSheet = go.AddComponent<DopeSheet>();

            return dopeSheet;
        }

        private void InitSpacer()
        {
            var go = new GameObject();
            go.transform.SetParent(transform, false);

            go.AddComponent<LayoutElement>().preferredHeight = 10f;
        }

        public void Bind(AtomAnimation animation)
        {
            _dopeSheet.Bind(animation);
        }
    }
}