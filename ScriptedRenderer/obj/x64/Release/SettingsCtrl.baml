   M S B A M L   `   `   `  ���� G  CZenithEngine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG3clr-namespace:ZenithEngine.UI;assembly=ZenithEngineZenithEngine.UI  I EScriptedRender, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:ScriptedRenderScriptedRender A0clr-namespace:ZenithEngine;assembly=ZenithEngineZenithEngine  !   ScriptedRender.SettingsCtrl   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     Bui3clr-namespace:ZenithEngine.UI;assembly=ZenithEngine     .localclr-namespace:ScriptedRender     @bme0clr-namespace:ZenithEngine;assembly=ZenithEngine       � 	Resources   �� MergedDictionaries �� Source �� 
TargetType   SubTabs �� BasedOn  SubTabItems��  ��  �� $8 2pack://siteoforigin:,,,/Languages/en/scripted.xaml?��� $D >pack://application:,,,/ZenithEngine;component/UI/Material.xaml?�%:   '��       7   '��      7  �� " ��8   �� " ��8   .��� .+��� $��10q�.@���   resourcesTab#���  .��� $��10q� � ColumnDefinitions �� $
��204* ��� $
��371* ��� $
��199* �.+�'   !ZenithEngine.NoteColorPalettePick  -     Name$ paletteList��$��0q�$��2��$��2��]� ��. $��2��.+��� -    ��Name$ reloadListButton��  
reloadList#���  $��0,0,0,0q�$��26��$	��Top=��� -   $ reloadPackButton��  reloadScript#���  $��0,10,0,0q�$��26��$	��Top=��� -   $ openFolderButton��  
openFolder#���  $��0,10,0,0q�$��26��$��Bottom=��� -    ��Name$ 
pluginList��$��0,10,0,0q�]� $��1����. .+��� -   	 ��Name$	 
pluginDesc��$	��100��$��Bottom=�
 �� TextAlignment$
 Center=�$��1��$��
10,10,10,0q� �� TextWrapping$
 Wrap=�{�.  $��1���� -    ��Name$ 
previewImg��$��1��$��	10,0,10,0q��� -   $��	switchTab��  settingsTab#���  .��]� $��
10,10,10,0q�.+�]� -	   $��profileDock��$y�	Collapsed=�$	��Top=���.  $��0,0,0,10q�.+���   profile#���  �� -
   $��profileSelect��$	��120��$��5,0,0,0q��� -   $��profileName��$��5,0,0,0q�$	��120���� -   $��saveProfile��$��5,0,0,0q�$
��20,0q�  save#���  �� -   $��deleteProfile��$��5,0,0,0q�$
��20,0q� 
	 delete#��� 	 �� -   $��profileDefaults��$��Right=�$
��20,0q� 
 defaults#��� 
 ]� -   $��settingsPanel����.  ��   miscTab#���  .���� $��10q�.+�]� $
��Left=���.  $��0,0,0,0q�$	��Top=�.+���   	firstNote#���  $
��Left=�$	��Top=�$
��Left=�"   ZenithEngine.UI.NumberSelect  -     Name$ 	firstNote��   Value$ 0~�   Maximum$	 254~�   Minimum$ 0~�$��5,0,0,0q�$
��Left=�$��80��$��26��$	��Top=���   lastNote#���  $
��Left=�$��10,0,0,0q�$	��Top=�  -   $ lastNote��$	 127~�$	 255~�$ 1~�$��5,0,0,0q�$
��Left=�$��80��$��26��$	��Top=�]� $
��Left=���.  $��0,10,0,0q�$	��Top=�$	��528��.+���   noteScreenTime#���  $
��Left=�$��0,0,0,0q�$	��Top=�!   ZenithEngine.UI.ValueSlider  -     Name$ noteDeltaScreenTime��   Maximum$ 12Q�   DecimalPoints$ 2��   Minimum$ 2Q�   TrueMin$ 1~�   TrueMax$ 100000~�$	��305��$	��Top=�