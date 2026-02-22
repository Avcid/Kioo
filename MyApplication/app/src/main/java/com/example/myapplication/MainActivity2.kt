package com.example.myapplication

import android.content.Intent
import android.os.Bundle
import android.util.Log
import android.widget.Toast
import androidx.activity.enableEdgeToEdge
import androidx.appcompat.app.AppCompatActivity
import androidx.core.view.ViewCompat
import androidx.core.view.WindowInsetsCompat
import androidx.lifecycle.lifecycleScope
import com.example.myapplication.databinding.ActivityMain2Binding
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import kotlinx.coroutines.withContext
import org.json.JSONObject
import java.net.HttpURLConnection
import java.net.URL
import kotlin.contracts.contract

class MainActivity2 : AppCompatActivity() {
    private lateinit var binding: ActivityMain2Binding
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityMain2Binding.inflate(layoutInflater)
        setContentView(binding.root)

        binding.tvToLogin.setOnClickListener {
            val intent = Intent(this, MainActivity::class.java)
            startActivity(intent)
            finish()
        }

        binding.btnRegister.setOnClickListener {
            performRegister()
        }
    }

    private fun performRegister() {
        val fullName = binding.etFullName.text.toString().trim()
        val email = binding.etEmail.text.toString().trim()
        val numberPhone = binding.etNumberPhone.text.toString().trim()
        val password = binding.etPassword.text.toString().trim()
        val confirmPassword = binding.etConPassword.text.toString().trim()

        if (fullName.isEmpty() || email.isEmpty() || numberPhone.isEmpty() || password.isEmpty()) {
            Toast.makeText(this, "Please ill all fields", Toast.LENGTH_SHORT).show()
            return
        }

        if (confirmPassword != password) {
            Toast.makeText(this, "Passwords do not match!", Toast.LENGTH_SHORT).show()
            return
        }

        lifecycleScope.launch(Dispatchers.IO) {
            try {
                val url = URL("http://10.0.2.2:5000/api/register")
                val conn = url.openConnection() as HttpURLConnection
                conn.requestMethod = "POST"
                conn.setRequestProperty("Content-Type", "application/json")
                conn.doOutput = true

                val dataRegister = JSONObject().apply {
                    put("email", email)
                    put("fullname", fullName)
                    put("PhoneNumber", numberPhone)
                    put("Password", password)
                    put("ConfirmPassword", confirmPassword)
                }

                conn.outputStream.use { os ->
                    os.write(dataRegister.toString().toByteArray())
                }

                val responseCode = conn.responseCode

                withContext(Dispatchers.Main) {
                    if (responseCode == HttpURLConnection.HTTP_OK) {
                        val intent = Intent(this@MainActivity2, MainActivity::class.java)
                        Toast.makeText(this@MainActivity2, "Success", Toast.LENGTH_SHORT).show()
                        startActivity(intent)
                        finish()

                    } else {
                        val errorInput = conn.errorStream?.bufferedReader()?.readText() ?: "No error message"
                        Log.e("API_ERROR", "Code: $responseCode, Message: $errorInput")
                        Toast.makeText(this@MainActivity2, "Gagal $responseCode: Lihat Logcat", Toast.LENGTH_LONG).show()
                    }
                }

            } catch (e: Exception) {
                withContext(Dispatchers.Main) {
                    Toast.makeText(this@MainActivity2, "Error: ${e.message}", Toast.LENGTH_SHORT)
                        .show()
                }
            }
        }
    }
}