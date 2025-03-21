using System;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(Modifier))]
public class ModifierPropertyDrawer : PropertyDrawer
{
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        var container = new VisualElement();
        
        var types = TypeCache.GetTypesDerivedFrom<Modifier>();

        var currentType = property.managedReferenceValue?.GetType();
        
        var dropdown = new DropdownField("Modifier Type", types.Select(t => t.Name).ToList(), currentType == null ? 0 : types.IndexOf(currentType));
        
        var propertyField = new PropertyField(property);
        propertyField.Bind(property.serializedObject);
        
        
        if (currentType == null)
            CreateModifierObject(property, types[0], propertyField);
        
        dropdown.RegisterValueChangedCallback((_) => CreateModifierObject(property, types[dropdown.index], propertyField));
        
        container.Add(dropdown);
        container.Add(propertyField);
        
        return container;
    }

    private void CreateModifierObject(SerializedProperty property, Type newType, PropertyField field)
    {
        if (property.managedReferenceValue?.GetType() == newType)
            return;

        property.managedReferenceValue = Activator.CreateInstance(newType);
        property.serializedObject.ApplyModifiedProperties();
        field.Bind(property.serializedObject);
    }
}
