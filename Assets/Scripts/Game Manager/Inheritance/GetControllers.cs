using UnityEngine;
namespace Controllers
{
    class GetControllers : MonoBehaviour
    {
        protected HotAndColdController HotAndColdController { get { return GetComponent<HotAndColdController>(); }}
        protected CanvasController CanvasController { get {return GetComponent<CanvasController>(); }}
        protected Timer Timer { get { return GetComponent<Timer>(); }}
        protected LevelController LevelController { get { return GetComponent<LevelController>(); }}
        protected NPCController NPCController { get { return GetComponent<NPCController>(); }}
        protected QuestController QuestController { get { return GetComponent<QuestController>(); }}
        protected EventController EventController { get { return GetComponent<EventController>(); }}
        protected SaveController SaveController { get { return GetComponent<SaveController>(); }}
        protected ButtonController ButtonController { get { return GetComponent<ButtonController>(); }}
        protected DevToolController DevToolController { get { return GetComponent<DevToolController>(); }}
    }

    interface ControllersTemplate
    {
        void StartScript();
        void Reset();
    }
}
