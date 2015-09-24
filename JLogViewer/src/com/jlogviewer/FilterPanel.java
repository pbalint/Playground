package com.jlogviewer;

import java.awt.Color;
import java.awt.GridBagConstraints;
import java.awt.GridBagLayout;
import java.awt.Insets;

import javax.swing.DefaultListModel;
import javax.swing.JButton;
import javax.swing.JList;
import javax.swing.JPanel;
import javax.swing.JTextField;
import javax.swing.ListModel;
import javax.swing.border.LineBorder;

import java.awt.Dimension;
import java.awt.event.ActionListener;
import java.awt.event.ActionEvent;
import java.util.Set;
import java.util.TreeSet;

@SuppressWarnings( "serial" )
public class FilterPanel extends JPanel {
    private JTextField      textEditNewFilter;
    private JList< String > jListFilters;
    private DefaultListModel< String > filtersModel = new DefaultListModel<>();
            
    public FilterPanel( Set< String > filters ) {
        setPreferredSize( new Dimension( 320, 240 ) );
        GridBagLayout gridBagLayout = new GridBagLayout();
        gridBagLayout.columnWidths = new int[] { 0, 0 };
        gridBagLayout.rowHeights = new int[] { 0, 0 };
        gridBagLayout.columnWeights = new double[] { 1.0, 0.0 };
        gridBagLayout.rowWeights = new double[] { 0.0, 1.0 };
        setLayout( gridBagLayout );

        textEditNewFilter = new JTextField();
        GridBagConstraints gbc_textEditNewFilter = new GridBagConstraints();
        gbc_textEditNewFilter.weightx = 1.0;
        gbc_textEditNewFilter.insets = new Insets( 0, 0, 5, 5 );
        gbc_textEditNewFilter.fill = GridBagConstraints.HORIZONTAL;
        gbc_textEditNewFilter.gridx = 0;
        gbc_textEditNewFilter.gridy = 0;
        add( textEditNewFilter, gbc_textEditNewFilter );
        textEditNewFilter.setColumns( 10 );

        JButton buttonAddFilter = new JButton( "Add" );
        buttonAddFilter.addActionListener( new ActionListener() {
            public void actionPerformed( ActionEvent arg0 ) {
                if ( textEditNewFilter.getText().length() > 0 && !filtersModel.contains( textEditNewFilter.getText() ) ) {
                    filtersModel.add( 0, textEditNewFilter.getText() );
                    textEditNewFilter.requestFocus();
                }
            }
        } );
        GridBagConstraints gbc_buttonAddFilter = new GridBagConstraints();
        gbc_buttonAddFilter.fill = GridBagConstraints.HORIZONTAL;
        gbc_buttonAddFilter.insets = new Insets( 0, 0, 5, 0 );
        gbc_buttonAddFilter.gridx = 1;
        gbc_buttonAddFilter.gridy = 0;
        add( buttonAddFilter, gbc_buttonAddFilter );

        jListFilters = new JList<>();
        jListFilters.setBorder( new LineBorder( Color.LIGHT_GRAY ) );
        GridBagConstraints gbc_jListFilters = new GridBagConstraints();
        gbc_jListFilters.weighty = 1.0;
        gbc_jListFilters.weightx = 1.0;
        gbc_jListFilters.insets = new Insets( 0, 0, 0, 5 );
        gbc_jListFilters.fill = GridBagConstraints.BOTH;
        gbc_jListFilters.gridx = 0;
        gbc_jListFilters.gridy = 1;
        add( jListFilters, gbc_jListFilters );

        JButton buttonRemoveFilter = new JButton( "Remove" );
        buttonRemoveFilter.addActionListener( new ActionListener() {
            public void actionPerformed( ActionEvent e ) {
                int selectedFilterIndex = jListFilters.getSelectedIndex();
                if ( selectedFilterIndex != -1 ) {
                    filtersModel.removeElementAt( selectedFilterIndex );
                }
                if ( filtersModel.getSize() > 0 ) {
                    jListFilters.setSelectedIndex( Math.min( selectedFilterIndex, filtersModel.getSize() - 1 ) );
                }
            }
        } );
        GridBagConstraints gbc_buttonRemoveFilter = new GridBagConstraints();
        gbc_buttonRemoveFilter.anchor = GridBagConstraints.NORTH;
        gbc_buttonRemoveFilter.gridx = 1;
        gbc_buttonRemoveFilter.gridy = 1;
        add( buttonRemoveFilter, gbc_buttonRemoveFilter );

        for ( String filter : filters ) {
            filtersModel.addElement( filter );
        }
        jListFilters.setModel( filtersModel );
    }

    public Set< String > getFilters() {
        Set< String > filters = new TreeSet<>();
        ListModel< String > model = jListFilters.getModel();
        for ( int i = 0; i < model.getSize(); i++ ) {
            filters.add( model.getElementAt( i ) );
        }
        return filters;
    }
}
