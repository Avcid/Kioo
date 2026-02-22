package com.example.latihangawean4

import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import com.example.latihangawean4.databinding.ActivityMain4Binding
import com.example.latihangawean4.databinding.FragmentBlank4Binding

class BlankFragment4 : Fragment() {
    private lateinit var binding: FragmentBlank4Binding
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

    }

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        binding = FragmentBlank4Binding.inflate(inflater)

        return binding.root
    }
}