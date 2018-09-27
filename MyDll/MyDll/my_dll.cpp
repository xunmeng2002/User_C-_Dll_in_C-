#define PB_API __declspec(dllexport) 

extern "C"
{
	typedef PB_API int(*SumFunc)(int a, int b);
	typedef PB_API int(*MinusFunc)(int a, int b);
	typedef PB_API int(*MultiFunc)(int a, int b);
	typedef PB_API int(*DivideFunc)(int a, int b);
	typedef PB_API struct
	{
		SumFunc sum_func;
		MinusFunc minus_func;
		MultiFunc multi_func;
		DivideFunc divide_func;
	}FuncCallback;

	static FuncCallback* s_func_callback;

	PB_API void register_spi(FuncCallback* func_callback)
	{
		s_func_callback = func_callback;
	}
	PB_API int call_sum(int a, int b)
	{
		return s_func_callback->sum_func(a, b);
	}
	PB_API int call_minus(int a, int b)
	{
		return s_func_callback->minus_func(a, b);
	}
	PB_API int call_multi(int a, int b)
	{
		return s_func_callback->multi_func(a, b);
	}
	PB_API int call_divide(int a, int b)
	{
		return s_func_callback->divide_func(a, b);
	}


	PB_API int sum(int a, int b)
	{
		return a + b;
	}
	PB_API int sum_1(int* a, int* b)
	{
		return *a + *b;
	}
	PB_API int sum_2(SumFunc sum_func, int a, int b)
	{
		return sum_func(a, b);
	}
}