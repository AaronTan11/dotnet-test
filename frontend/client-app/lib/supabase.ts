// import "react-native-url-polyfill/auto";
import * as SecureStore from "expo-secure-store";
import { createClient } from "@supabase/supabase-js";
import { Platform } from "react-native";

const supabaseUrl = "https://natvmrwqcdzlfcmgbsxc.supabase.co";
const supabaseAnonKey =
    "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Im5hdHZtcndxY2R6bGZjbWdic3hjIiwicm9sZSI6ImFub24iLCJpYXQiOjE2ODYxMzQzNjUsImV4cCI6MjAwMTcxMDM2NX0.D4UBLoqa7RwenVtejQQYHRF5lHa6ztyHH_KSieP-774";
let options = {};

if (Platform.OS !== "web") {
    const secureStoreAdapter = {
        setItem: async (key: string, value: string) =>
            await SecureStore.setItemAsync(key, value),
        getItem: async (key: string) => await SecureStore.getItemAsync(key),
        removeItem: async (key: string) =>
            await SecureStore.deleteItemAsync(key),
    };

    options = {
        auth: {
            storage: secureStoreAdapter,
            autoRefreshToken: true,
            persistSession: true,
            detectSessionInUrl: false,
        },
    };
}

export const supabase = createClient(supabaseUrl, supabaseAnonKey, options);
