package com.jlogviewer;

import javax.swing.JPanel;
import javax.swing.JComboBox;
import javax.swing.JLabel;
import javax.swing.JSpinner;
import javax.swing.UIManager;
import javax.swing.UIManager.LookAndFeelInfo;

import java.awt.GridBagLayout;
import java.awt.GridBagConstraints;
import java.awt.Insets;

@SuppressWarnings( "serial" )
public class ConfigurationPanel extends JPanel {
    private JSpinner                     maxLogEntryLengthValue;
    private JComboBox< LookAndFeelName > comboBoxLookAndFeel;

    private class LookAndFeelName {
        private String name;
        private String className;

        public LookAndFeelName( String name, String className ) {
            this.name = name;
            this.className = className;
        }

        public String getClassName() {
            return className;
        }

        @Override
        public String toString() {
            return name;
        }
    }

    public ConfigurationPanel( Config config ) {
        GridBagLayout gridBagLayout = new GridBagLayout();
        setLayout( gridBagLayout );

        JLabel labelLookAndFeel = new JLabel( "Look and feel" );
        GridBagConstraints gbc_labelLookAndFeel = new GridBagConstraints();
        gbc_labelLookAndFeel.anchor = GridBagConstraints.NORTHWEST;
        gbc_labelLookAndFeel.insets = new Insets( 5, 5, 5, 5 );
        gbc_labelLookAndFeel.gridx = 1;
        gbc_labelLookAndFeel.gridy = 0;
        add( labelLookAndFeel, gbc_labelLookAndFeel );

        comboBoxLookAndFeel = new JComboBox<>();
        GridBagConstraints gbc_comboBoxLookAndFeel = new GridBagConstraints();
        gbc_comboBoxLookAndFeel.insets = new Insets( 0, 0, 5, 0 );
        gbc_comboBoxLookAndFeel.fill = GridBagConstraints.HORIZONTAL;
        gbc_comboBoxLookAndFeel.gridx = 2;
        gbc_comboBoxLookAndFeel.gridy = 0;
        add( comboBoxLookAndFeel, gbc_comboBoxLookAndFeel );

        JLabel labelLogEntryLength = new JLabel( "Maximum log message lenth to display" );
        GridBagConstraints gbc_labelLogEntryLength = new GridBagConstraints();
        gbc_labelLogEntryLength.anchor = GridBagConstraints.WEST;
        gbc_labelLogEntryLength.insets = new Insets( 5, 5, 5, 5 );
        gbc_labelLogEntryLength.gridx = 1;
        gbc_labelLogEntryLength.gridy = 1;
        add( labelLogEntryLength, gbc_labelLogEntryLength );

        maxLogEntryLengthValue = new JSpinner();
        GridBagConstraints gbc_maxLogEntryLengthValue = new GridBagConstraints();
        gbc_maxLogEntryLengthValue.anchor = GridBagConstraints.NORTHWEST;
        gbc_maxLogEntryLengthValue.gridx = 2;
        gbc_maxLogEntryLengthValue.gridy = 1;
        add( maxLogEntryLengthValue, gbc_maxLogEntryLengthValue );

        for ( LookAndFeelInfo laf : UIManager.getInstalledLookAndFeels() ) {
            comboBoxLookAndFeel.addItem( new LookAndFeelName( laf.getName(), laf.getClassName() ) );
            if ( config.getLookAndFeel().equals( laf.getClassName() ) ) {
                comboBoxLookAndFeel.setSelectedIndex( comboBoxLookAndFeel.getItemCount() - 1 );
            }
        }
        comboBoxLookAndFeel.setSelectedItem( config.getLookAndFeel() );
        maxLogEntryLengthValue.setValue( config.getMaxLogEntryLength() );
    }

    public void applyConfig( Config config ) {
        config.setLookAndFeel( ( (LookAndFeelName)comboBoxLookAndFeel.getSelectedItem() ).getClassName() );
        config.setMaxLogEntryLength( Integer.parseInt( maxLogEntryLengthValue.getValue().toString() ) );
    }
}
