   M S B A M L   `   `   `  ����C  ?PokeWish, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:PokeWish.MVVM.ViewModelPokeWish.MVVM.ViewModel  5   	   7 clr-namespace:PokeWish.MVVM.ViewPokeWish.MVVM.View  5   	       PokeWish.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   4VM%clr-namespace:PokeWish.MVVM.ViewModel     5   	   1view clr-namespace:PokeWish.MVVM.View     5   	     � Title$  PokeWish��5   	   $	��450��6   $	��800��6'    � WindowStyle$
 None=�5   	    � 
ResizeMode$ NoResize=�5   	    � DataContext 5	      *   $PokeWish.MVVM.ViewModel.MainWindowVM  5
   
   .��5      � .+�5   
   � -   5        $��Metabuttons��5   H   ���#2a453b$��30��6$   $	��Top=�60    K� ZIndex$ 2��6[   .+�5      �� $��PokeWish��6   $��Center=�5      
�������$
��Bold"�5      $��10,0,0,0q�5      �� 5       �� Orientation$ 
Horizontal=�6   $��Right=�62   .+�5      �� -   5        $	��—��5      $��30��5      $��30��6$   
������ 
�������$
��Bold"�5       � ToolTip$ Réduire��5      $��0q�5       �� 5#      -   5        $��X��5#      $��30��5$      $��30��6$   
������ 
�������$
��Bold"�5'      $ Fermer��5)      $��0q�5*      5+      5,      �� 5.   
   -   5         ��Name$ MainContent��5.      $��0,30,0,0q�6N   ��6.   �� *CurrentView+6N   %   PokeWish.MVVM.View.MusicControl  5/   
   $��Right=�6   $	��Top=�50      $��10q�51      $ 1��52      53      54      