using System;
using System.Diagnostics;
using System.Windows.Forms;
using static Tensorflow.Binding;
using Tensorflow.NumPy;
using Tensorflow;

namespace NeuroTesting
{
    internal static class Program
    {
        static void Main()
        {
            //Application.Run(new Form1());

            // Create a tensor holds a scalar value
            var t1 = new Tensor(3);

            // Init from a string
            var t2 = new Tensor("Hello! TensorFlow.NET");

            // Tensor holds a ndarray
            var nd = new NDArray(new int[] { 3, 1, 1, 2 });
            var t3 = new Tensor(nd);

            var c1 = tf.constant(3); // int
            var c2 = tf.constant(1.0f); // float
            var c3 = tf.constant(2.0); // double
            var c4 = tf.constant("Big Tree"); // string

            Debug.WriteLine($"t1: {t1}, t2: {t2}, t3: {t3}");
            Debug.WriteLine($"c1: {c1}, c2: {c2}, c3: {c3}, c4: {c4} ");
        }
    }
}