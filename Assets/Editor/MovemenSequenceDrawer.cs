// using UnityEditor;
// using UnityEditor.UIElements;
// using UnityEngine.UIElements;

// // MovementSequenceDrawerUIE
// [CustomPropertyDrawer(typeof(MovementSequence))]
// public class MovementSequenceDrawer : PropertyDrawer
// {
//     public override VisualElement CreatePropertyGUI(SerializedProperty property)
//     {
//         // Create property container element.
//         var container = new VisualElement();

//         // Create property fields.
//         var defaultField = new PropertyField(property.FindPropertyRelative("useDefaultSpeed"));
//         var startingSpeedField = new PropertyField(property.FindPropertyRelative("startingMoveSpeed"));
//         var maxSpeedField = new PropertyField(property.FindPropertyRelative("maxMoveSpeed"));
//         var accelerationField = new PropertyField(property.FindPropertyRelative("acceleration"));



//         // Add fields to the container.
//         container.Add(defaultField);
//         container.Add(startingSpeedField);
//         container.Add(maxSpeedField);
//         container.Add(accelerationField);

//         return container;
//     }
// }