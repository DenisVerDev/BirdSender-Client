   M S B A M L   `   `   `  ����J  FMessangerClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:MessangerClient.ControlsMessangerClient.Controls  5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   ComboBoxToggleButton  �� 
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
 cmuser�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      0local&clr-namespace:MessangerClient.Controls  5      %�  &	        &	     '�� o    7   7  '�� �
    &	     &	     &	 L    7  7  7  &	 �    7  &	 �    &	 �    &	 �    &		 +    &	
 �    &	 �    &	 )    &	 �    '�� �    'm� �    &	 x    &	 �    &	 �    �� 5      "  d�63   .R�5   
   �  5      �� 5	      �� 5
      $��20 �6#   .+�5      �� $ Border��5      $��2��5      $ 0��5      
����������#4ABFB6$��1q�5      �� 5      $��0��5      $ 0��5      $��1q�5      
����������#4ABFB6$��0,0,1,0q�5      F� 5      $ Arrow��5      $��1��5      ���#4ABFB6$��Center=�5      $��Center=�5      =����        �    �� � 	= ��[ � z �� � z     � 	=  	= 	5       5"      5#      �� 5%      "  ��6.   .R�5&   
   �� $ PART_ContentHost��6   |�.  #��z��6=   5'      �� 5)      " ��6&   .�5*   
   ��  �@J�SnapsToDevicePixels . 60   �� 5+   
    �@�OverridesDefaultStyle . 62   �� 5,   
    � ��$
 Auto=�6G   �� 5-   
    � ��$
 Auto=�6E   �� 5.   
    � �� . 6:   �� 5/   
    � ��$	 120��6%   �� 50   
    � ��$ 20��6&   �� 51   
    � �� �#4ABFB66'   �� 52   
    � �� 53      �� 54      "  ��6"   .R�55      � .+�56      d� $��ToggleButton��57      8��  $��2��59      |�.  $ Press=�5<      	 5;      �� $
 IsDropDownOpen�$ TwoWay=� �� *TemplatedParent+5=      �� 5>      $��ContentSite��6+    .  #��z 6X   #��z 5?      #��z��5@      $��3,3,23,3q�5A      $��Center=�5B      $
��Left=�5C      �� 5D      $ PART_EditableTextBox��6"   8�� $
��Left=�5G      $��Center=�5H      $��3,3,23,3q�5I      |�. 
�������
��� � �$y�Hidden=�5M      # z 5N      ��5E      Z� 5N      � 5O      $��Popup��5P      $��Bottom=�5Q      #��z 5R       . |�.  $��Slide=�5U      .��5W      � $��DropDown��6#    . #��z 5Y      #��z 5Z      .+�5[   "   �� $ DropDownBorder��5\   !   
�������$��1q�5_   !   ���#4ABFB65`   !   �� 5a   "   $��4,6,4,6q�6/    . .��5b   &   ��  . $ 	Contained=�6D   5c   #   5d      5e      5f       5g      P� 5h       �@��HasItems .  . �5i      �� $ DropDownBorder��6%    � ��$ 95��6V   5j      P� 5k       � {� .  . �5l      ��  � �� �#8888886;   5m      P� 5n       �@��
IsGrouping . . �5o      ��  � �� .  6N   5p      P� 5q      $ Popup��6"    �@�AllowsTransparency . . �5r      �� $ DropDownBorder��6%    �@��CornerRadius$ 0��6Y   �� 5s      $ DropDownBorder��6%    � ��$ 0,2,0,0q�6S   5t      P� 5u       �@��
IsEditable . . �5v      ��  � �� .  6:   �� 5w      $ PART_EditableTextBox��6%    � y�$ Visible=�6]   �� 5x      $ ContentSite��6%    � y�$ Hidden=�6T   5y      5{      5}       5~   
   5�      �� 5�      " ��6*   .�5�   
   ��  �@J�SnapsToDevicePixels . 60   �� 5�   
    � �� �#4ABFB66'   �� 5�   
    �@�OverridesDefaultStyle . 62   �� 5�   
    � �� 5�      �� 5�      "  ��6"   .R�5�      �� $��Border��6   $  2q�5�       . ._�5�      �� 5�       5�      P� 5�       �@��IsHighlighted . . �5�      �� $ Border��6%    � ��
 �����6O   5�      P� 5�       � {� .  . �5�      ��  � ��
 �����6;   5�      5�      5�      5�      �� 5�      " ��6-   .�5�   
   ��  � �� .  6&   �� 5�   
    � |� .  6&   �� 5�   
    � ��$ 10,0,2,0q�6#   �� 5�   
    � �� 5�      �� 5�      "  ��6"   .R�5�      �� ���#E6E6E664   5�      5�      5�      �� 5�      " n�6(   .�5�   
   ��  � �� .  6&   �� 5�   
    � |� .  6&   �� 5�   
    � ��$ 10,0,2,0q�6#   �� 5�   
    � �� 5�      �� 5�      "  n�6"   .R�5�      �� ���#E6E6E6._�5�      �� $ 2��6!   ���#9A9A9A6I   5�      5�      5�      5�      �� 5�      "  ��60   .R�5�   
   � ! 5�      �� 5�      $��* �6    .+�5�      ]� $��
PART_Track��6   $��0��6&   ��. " 5�      �� 5�      $��ScrollBar.PageUpCommand��6#   8��  5�      # 5�      n� 5�      8�� 5�      $ 5�      �� 5�      $��ScrollBar.PageDownCommand��6#   8�� 5�      5�      5�      5�      �� 5�      "  ��6&   .R�5�   
   � $
% grid��6   .+�5�      �� ._�5�      �� 5�      �� 5�      $& PART_VerticalScrollBar��6c   8��  $��Right=�6F   #( z' 5�      #* z) 5�      #, z+ 5�      #y�z- 5�      5�      5�      �� 5�      "  ��6   .R�5�   
   �� $
 body��6*   
������ ._�5�      �� #��z��6   6C   5�       5�   
   P� 5�       �@J�IsMouseOver . . �5�      �� $
 body��6    � �� �#4ABFB66S   5�      5�      �� 5�      "  ��6   .R�5�   
   �� $
 body��6*   
������ ._�5�      �� #��z��6   6C   5�       5�   
   P� 5�       �@J�IsMouseOver . . �5�      �� $
 body��6    � �� �#FD878D6S   5�      5�      �� 5�      "  ��6(   .R�5�   
   � .+�5�      �� $. PART_ContentHost��6�   #��z��6   #��z��6S   #��z��6�   6�   5�      5�      �� 5�      "  ��6'   .R�5�   
   �� $
 body��6   #��z��6   ._�5�      �� #��z��6   6E   5�      5�      �� 5     "  ��6-   .R�5  
   �� $
 body��6   
�������#��z��62   $��2q�6^   $ 20��6r   ._�5     �� $/ content��6   #��z��60   6V   5      5  
   P� 5      �@J�IsMouseOver . . �5     �� $
 body��6    � ��$ 3q�6F   6R   5     5
     �� 5     "  ��6)   .R�5  
   �� $
 body��6   #��z��6   $ 10��6I   ._�5     �� $��Center=�6   $��Center=�68   #��z��6U   6{   5      5  
   P� 5      �@J�IsMouseOver . . �5     �� $
 body��6    � �� �#00A99D6S   �� 5      �@�Cursor$
 Hand��6+   6:   5     5     �� 5     "  ��6*   .R�5  
   �� $
 body��6   #��z��6   $ 10��6I   ._�5     �� $0 content��6   $��Center=�6.   $��Center=�6I   #��z��6f   6�   5      5  
   P� 5      �@J�IsMouseOver . . �5     �� $
 body��6    � ��$ 1q�6F   6R   �� 5      �@�Cursor$
 Hand��6+   6:   5      5"     �� 5$     "  �6'   .R�5%  
   � $
% Root��6   .+�5&     �� $ 
PART_Track��6   $ 4��6*   #��z��6;   $��2q�6f   �� 5'     $ PART_Indicator��6   $
��Left=�6-   $	��1.5q�6H   $ 3��6^   #��z��6o   5(     5)     �� 5/     " ��6   .�50  
   ��  � �� 51     �� 52     "  ��6#   .R�53     �� $	��150��6   $ 5��6)   $��0,0,0,3q�6:   
�������
�������$��0,5,0,0q�6   ._�54     ��  . $ Cycle=�68   55     56     58     59     �� 5;     " m�6   .�5<  
   ��  � �� 5=     �� 5>     "  m�6"   .R�5?     �� $
 body��6   $ 5��6+   
�������._�5@     �� $1 	lbcontent��6    $
��Left=�63   #��z��6N   $
��11pt(�6r   $��Arial)�6�   ���#6CC4C76�   5A      5B     P� 5C      �@J�IsMouseOver . . �5D     �� $
 body��6%    � �� �#6CC4C76M   �� 5E     $ 	lbcontent��6%    � ��
 �����6R   5F     5H     5J     5K     �� 5N     .@�5O  
   m� $2 0��6   $��Delete chat��6   5P     �� 5R     .@�5S  
   m� $2 3��6   $��Invite user��6   m� 5T  
   $2 2��6   $��
Kick users��6   m� 5U  
   $2 1��6   $��Group settings��6   m� 5V  
   $2 0��6   $��Delete group��6   5W     �� 5Y     .@�5Z  
   m� $2 4��6   $��Leave group��6   5[     5]     