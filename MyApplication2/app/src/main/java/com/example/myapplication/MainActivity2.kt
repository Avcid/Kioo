package com.example.myapplication

import android.content.Context
import android.content.Intent
import android.os.Bundle
import androidx.appcompat.app.AppCompatActivity
import com.example.myapplication.databinding.ActivityMain2Binding

class MainActivity2 : AppCompatActivity() {

    private lateinit var binding: ActivityMain2Binding

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        binding = ActivityMain2Binding.inflate(layoutInflater)
        setContentView(binding.root)

        val sharedPref = getSharedPreferences("USER_PREF", Context.MODE_PRIVATE)
        val namaUser = sharedPref.getString("NAMA_USER", "User")
        val tglLahir = sharedPref.getString("TGL_LAHIR", "-")

        binding.tvWelcome.text = "Selamat datang, $namaUser"
        binding.tvTanggalLahir.text = "Tanggal lahir: $tglLahir"

        binding.btnLogout.setOnClickListener {
            sharedPref.edit().clear().apply()
            startActivity(Intent(this, MainActivity::class.java))
            finish()
        }
    }
}
