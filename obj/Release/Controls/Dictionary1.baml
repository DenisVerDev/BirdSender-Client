   M S B A M L   `   `   `  ���� J  FMessangerClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:MessangerClient.ControlsMessangerClient.Controls  R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   ComboBoxToggleButton  �� 
TargetType � ColumnDefinitions ��Name �� CornerRadius F�Name  ComboBoxTextBox �� 
TargetType �� Property �� Value �� 	ClickMode	 d� 	IsChecked
 �� Path �� Mode �� RelativeSource J� IsHitTestVisible �� SelectionBoxItem �� SelectionBoxItemTemplate ��Name �� 
IsReadOnly �� 
IsReadOnly �� IsDropDownOpen � AllowsTransparency J� SnapsToDevicePixels � ActualWidth �� MaxDropDownHeight K� IsItemsHost-   'System.Windows.Input.KeyboardNavigation    DirectionalNavigation �� Triggers P� Property P� Value �� 
TargetName P� 
SourceName �� Triggers  �� Padding  ScrollBarPageButtonStyle  ScrollBarThumbStyle  VerticalScrollBar! � RowDefinitions" ]� DecreaseRepeatButton# ]� Thumb$ ]� IncreaseRepeatButton 
 scroll% �Name& ��Name' �� VerticalOffset( � Value) �� ScrollableHeight* � Maximum+ �� ViewportHeight, �� ViewportSize*- �� #ComputedVerticalScrollBarVisibility  topbartemplate  closetemplate  	textplace. ��Name 	 btnempty 
 btncircleempty/ ��Name  
birdbutton  birdbutton20 ��Name  pbcustom1 ��Name 
 cmchat
2 � Tag  cmadmin 
 cmuser�� D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     0local&clr-namespace:MessangerClient.Controls  %�  &	        &	 5    '�� l    7   7  '�� �    &	 �    &	 t    &	 	    7  7  7  &	 �	    7  &	 E
    &	 �
    &	 O    &		 �    &	
 �    &	 l    &	 /    &	 �    '�� �    'm�     &	 '    &	 N    &	 �    �� "  d�.R��  �� �� $��20 �.+��� $ Border��$��2��$ 0��
����������#4ABFB6$��1q��� $��0��$ 0��$��1q�
����������#4ABFB6$��0,0,1,0q�F� $ Arrow��$��1�����#4ABFB6$��Center=�$��Center=�=����        �    �� � 	= ��[ � z �� � z     � 	=  	= 	�� "  ��.R��� $ PART_ContentHost��|�.  #��z���� " ��.���  �@J�SnapsToDevicePixels . ��  �@�OverridesDefaultStyle . ��  � ��$
 Auto=���  � ��$
 Auto=���  � �� . ��  � ��$	 120����  � ��$ 20����  � �� �#4ABFB6��  � �� �� "  ��.R�� .+�d� $��ToggleButton��8��  $��2��|�.  $ Press=�	 �� $
 IsDropDownOpen�$ TwoWay=� �� *TemplatedParent+�� $��ContentSite�� .  #��z #��z #��z��$��3,3,23,3q�$��Center=�$
��Left=��� $ PART_EditableTextBox��8�� $
��Left=�$��Center=�$��3,3,23,3q�|�. 
�������
��� � �$y�Hidden=�# z ��Z� � $��Popup��$��Bottom=�#��z  . |�.  $��Slide=�.��� $��DropDown�� . #��z #��z .+��� $ DropDownBorder��
�������$��1q����#4ABFB6�� $��4,6,4,6q� . .����  . $ 	Contained=� P�  �@��HasItems .  . ��� $ DropDownBorder�� � ��$ 95��P�  � {� .  . ���  � �� �#888888P�  �@��
IsGrouping . . ���  � �� .  P� $ Popup�� �@�AllowsTransparency . . ��� $ DropDownBorder�� �@��CornerRadius$ 0���� $ DropDownBorder�� � ��$ 0,2,0,0q�P�  �@��
IsEditable . . ���  � �� .  �� $ PART_EditableTextBox�� � y�$ Visible=��� $ ContentSite�� � y�$ Hidden=� �� " ��.���  �@J�SnapsToDevicePixels . ��  � �� �#4ABFB6��  �@�OverridesDefaultStyle . ��  � �� �� "  ��.R��� $��Border��$  2q� . ._���  P�  �@��IsHighlighted . . ��� $ Border�� � ��
 �����P�  � {� .  . ���  � ��
 ������� " ��.���  � �� .  ��  � |� .  ��  � ��$ 10,0,2,0q���  � �� �� "  ��.R��� ���#E6E6E6�� " n�.���  � �� .  ��  � |� .  ��  � ��$ 10,0,2,0q���  � �� �� "  n�.R��� ���#E6E6E6._��� $ 2�����#9A9A9A�� "  ��.R�� ! �� $��* �.+�]� $��
PART_Track��$��0����. " �� $��ScrollBar.PageUpCommand��8��  # n� 8�� $ �� $��ScrollBar.PageDownCommand��8�� �� "  ��.R�� $
% grid��.+��� ._��� �� $& PART_VerticalScrollBar��8��  $��Right=�#( z' #* z) #, z+ #y�z- �� "  ��.R��� $
 body��
������ ._��� #��z�� P�  �@J�IsMouseOver . . ��� $
 body�� � �� �#4ABFB6�� "  ��.R��� $
 body��
������ ._��� #��z�� P�  �@J�IsMouseOver . . ��� $
 body�� � �� �#FD878D�� "  ��.R�� .+��� $. PART_ContentHost��#��z��#��z��#��z���� "  ��.R��� $
 body��#��z��._��� #��z���� "  ��.R��� $
 body��
�������#��z��$��2q�$ 20��._��� $/ content��#��z�� P�  �@J�IsMouseOver . . ��� $
 body�� � ��$ 3q��� "  ��.R��� $
 body��#��z��$ 10��._��� $��Center=�$��Center=�#��z�� P�  �@J�IsMouseOver . . ��� $
 body�� � �� �#00A99D��  �@�Cursor$
 Hand���� "  ��.R��� $
 body��#��z��$ 10��._��� $0 content��$��Center=�$��Center=�#��z�� P�  �@J�IsMouseOver . . ��� $
 body�� � ��$ 1q���  �@�Cursor$
 Hand���� "  �.R�� $
% Root��.+��� $ 
PART_Track��$ 4��#��z��$��2q��� $ PART_Indicator��$
��Left=�$	��1.5q�$ 3��#��z���� " ��.���  � �� �� "  ��.R��� $	��150��$ 5��$��0,0,0,3q�
�������
�������$��0,5,0,0q�._���  . $ Cycle=��� " m�.���  � �� �� "  m�.R��� $
 body��$ 5��
�������._��� $1 	lbcontent��$
��Left=�#��z��$
��11pt(�$��Arial)����#6CC4C7 P�  �@J�IsMouseOver . . ��� $
 body�� � �� �#6CC4C7�� $ 	lbcontent�� � ��
 ������� .@�m� $2 0��$��Delete chat���� .@�m� $2 3��$��Invite user��m� $2 2��$��
Kick users��m� $2 1��$��Group settings��m� $2 0��$��Delete group���� .@�m� $2 4��$��Leave group��