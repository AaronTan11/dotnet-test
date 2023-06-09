import React, { useState } from "react";
import { Button, TextInput, View } from "react-native";
import { useMutation } from "@tanstack/react-query";
import axios from "axios";

interface User {
  name: string;
  password: string;
  email: string;
}

const registerUser = async (userInfo: User) => {
  try {
    const response = await axios.post(
      `http://localhost:5145/RegisterUser?name=${encodeURIComponent(
        userInfo.name,
      )}&email=${encodeURIComponent(
        userInfo.email,
      )}&password=${encodeURIComponent(userInfo.password)}`,
    );
    return response.data;
  } catch (err) {
    // Check if error is an instance of AxiosError
    if (axios.isAxiosError(err)) {
      // Check if response exists
      if (err.response) {
        // Now we can be sure that err.response exists, so this code is type-safe
        throw err.response.data;
      } else {
        // Handle case where no response was received
        throw new Error("No response received from server");
      }
    } else {
      // You may want to handle other types of errors differently
      throw err;
    }
  }
};

export default function RegistrationForm() {
  const [name, setName] = useState("");
  const [password, setPassword] = useState("");
  const [email, setEmail] = useState("");

  const mutation = useMutation(registerUser, {
    onSuccess: (data: string) => {
      // Handle successful registration here, for example:
      console.log(data);
    },
    onError: (error: string) => {
      // Handle error here, for example:
      console.error(error);
    },
  });

  const onSubmit = () => {
    mutation.mutate({ name, password, email });
  };

  return (
    <View className="mx-12 p-12">
      <TextInput
        value={name}
        onChangeText={setName}
        placeholder="name"
        className="p-3"
      />
      <TextInput
        value={password}
        onChangeText={setPassword}
        placeholder="Password"
        className="p-3"
        secureTextEntry
      />
      <TextInput
        value={email}
        onChangeText={setEmail}
        placeholder="Email"
        className="p-3"
      />
      <Button onPress={onSubmit} title="Register" />
    </View>
  );
}
